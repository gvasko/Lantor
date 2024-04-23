using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lantor.DomainModel
{
    public interface ISampleRepository
    {
        MultilingualSample GetDefaultSamples();
        Alphabet GetDefaultAlphabet();
        HiDimBipolarVector? GetLanguageVectorFromCache(LanguageSample languageSample, Alphabet alphabet);
        void AddLanguageVectorToCache(LanguageSample languageSample, Alphabet alphabet, HiDimBipolarVector vector);
        Task<IList<MultilingualSample>> GetAllMultilingualSamplesAsync();
        Task<MultilingualSample?> GetMultilingualSampleAsync(int id);
        Task<LanguageSample?> GetLanguageSampleAsync(int id);
        Task UpdateMultilingualSample(MultilingualSample updated);
        Task<MultilingualSample> CreateMultilingualSample(MultilingualSample sample);
    }
}
