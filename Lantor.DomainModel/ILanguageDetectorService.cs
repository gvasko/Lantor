using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lantor.DomainModel
{
    public interface ILanguageDetectorService
    {
        Task<LanguageSimilarityResult> Detect(string sample);
        Task<LanguageSimilarityResult> Detect(int sampleId, int alphabetId, string sample);
    }
}
