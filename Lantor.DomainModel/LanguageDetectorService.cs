using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            LoggerService.Logger.Debug("Default detection begins: {samplePrefix}", sample[..16]);

            var alphabet = sampleRepository.GetDefaultAlphabet();
            LoggerService.Logger.Debug("Default alphabet loaded");

            var vectorBuilder = new LanguageVectorBuilder(alphabet);
            var sampleVector = vectorBuilder.BuildLanguageVector(sample);
            LoggerService.Logger.Debug("Sample vector built.");

            var languageSamples = sampleRepository.GetDefaultSamples();
            LoggerService.Logger.Debug("Default samples loaded, count = {count}.", languageSamples.Languages.Count);

            LanguageSimilarityValue[] similarityValues = new LanguageSimilarityValue[languageSamples.Languages.Count];
            int i = 0;
            foreach (var language in languageSamples.Languages) 
            {
                var languageVector = sampleRepository.GetLanguageVector(language, alphabet);
                if (languageVector == null)
                {
                    LoggerService.Logger.Debug("Language vector for {lang} not found in cache.", language.Name);
                    languageVector = vectorBuilder.BuildLanguageVector(language.Sample);
                    LoggerService.Logger.Debug("Language vector for {lang} generated.", language.Name);
                    sampleRepository.SetLanguageVector(language, alphabet, languageVector);
                    LoggerService.Logger.Debug("Language vector for {lang} stored in cache.", language.Name);
                }
                else
                {
                    LoggerService.Logger.Debug("Language vector for {lang} loaded from cache.", language.Name);
                }
                var similarity = languageVector.Similarity(sampleVector);
                similarityValues[i++] = new LanguageSimilarityValue(language.Name, similarity);
                LoggerService.Logger.Debug("Language vector similarity for {lang} calculated.", language.Name);
            }

            Array.Sort(similarityValues, (x, y) => x.Value < y.Value ? 1 : x.Value > y.Value ? -1 : 0);

            LoggerService.Logger.Debug("Similarity results sorted.");
            stopWatch.Stop();
            LoggerService.Logger.Debug("Detected in {elapsed} ms.", stopWatch.ElapsedMilliseconds);


            return new LanguageSimilarityResult(similarityValues);
        }
    }
}
