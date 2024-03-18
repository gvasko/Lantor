using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lantor.DomainModel
{
    public class LanguageDetectorService : ILanguageDetectorService
    {
        public LanguageDetectorService(ISampleRepository sampleRepository) 
        {
        }

        public LanguageSimilarityResult Detect(string sample)
        {
            var result = new LanguageSimilarityResult(
                new LanguageSimilarityValue("en", 0.15),
                new LanguageSimilarityValue("de", 0.11),
                new LanguageSimilarityValue("hu", 0.05));
            return result;
        }
    }
}
