using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lantor.DomainModel
{
    // TODO: fat interface candidate?
    public interface ISampleRepository
    {
        Task<Alphabet> GetDefaultAlphabetAsync();
        Task<Alphabet?> GetAlphabetAsync(int id);
        Task<IList<Alphabet>> GetAllAlphabetsAsync();
        Task<Alphabet> CreateAlphabetAsync(string name, int dim);

        Task<MultilingualSample> GetDefaultSamplesAsync();
        Task<IList<MultilingualSample>> GetAllMultilingualSamplesAsync();
        Task<MultilingualSample?> GetMultilingualSampleAsync(int id);
        Task UpdateMultilingualSampleAsync(MultilingualSample updated);
        Task<MultilingualSample> CreateMultilingualSampleAsync(MultilingualSample sample);

        Task<LanguageSample?> GetLanguageSampleAsync(int id);
        Task UpdateLanguageSampleAsync(LanguageSample updated);
        Task<LanguageSample> CreateLanguageSampleAsync(LanguageSample sample);

        Task<HiDimBipolarVector?> GetLanguageVectorFromCacheAsync(LanguageSample languageSample, Alphabet alphabet);
        Task AddLanguageVectorToCacheAsync(LanguageSample languageSample, Alphabet alphabet, HiDimBipolarVector vector);
    }
}
