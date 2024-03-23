using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lantor.DomainModel
{
    public class LanguageDetectorService : ILanguageDetectorService
    {
        private readonly ISampleRepository sampleRepository;

        public LanguageDetectorService(ISampleRepository sampleRepository) 
        {
            this.sampleRepository = sampleRepository;
        }

        public LanguageSimilarityResult Detect(string sample)
        {
            var alphabet = sampleRepository.GetDefaultAlphabet();
            var vectorBuilder = new LanguageVectorBuilder(alphabet);
            var sampleVector = vectorBuilder.BuildLanguageVector(sample);

            var languageSamples = sampleRepository.GetDefaultSamples();

            LanguageSimilarityValue[] similarityValues = new LanguageSimilarityValue[languageSamples.Languages.Count];
            int i = 0;
            foreach (var language in languageSamples.Languages) 
            {
                var languageVector = sampleRepository.GetLanguageVector(language, alphabet);
                if (languageVector == null)
                { 
                    languageVector = vectorBuilder.BuildLanguageVector(language.Sample);
                    sampleRepository.SetLanguageVector(language, alphabet, languageVector);
                }
                var similarity = languageVector.Similarity(sampleVector);
                similarityValues[i++] = new LanguageSimilarityValue(language.Name, similarity);
            }

            Array.Sort(similarityValues, (x, y) => x.Value < y.Value ? 1 : x.Value > y.Value ? -1 : 0);

            return new LanguageSimilarityResult(similarityValues);
        }
    }
}
