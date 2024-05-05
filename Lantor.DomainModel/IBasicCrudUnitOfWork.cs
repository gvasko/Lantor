using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lantor.DomainModel
{
    public interface IBasicCrudUnitOfWork
    {
        Task<Alphabet?> GetAlphabetAsync(int id);
        Task<IList<Alphabet>> GetAllAlphabetListInfoAsync();
        Task<Alphabet> CreateAlphabetAsync(Alphabet alphabet);
        Task RemoveAlphabetAsync(int alphabetId);

        Task<IList<MultilingualSample>> GetAllMultilingualSamplesAsync();
        Task<MultilingualSample?> GetMultilingualSampleAsync(int id);
        void UpdateMultilingualSample(MultilingualSample updated);
        Task<MultilingualSample> CreateMultilingualSampleAsync(MultilingualSample sample);
        Task RemoveCascadeMultilingualSampleAsync(int multilingualSampleId);

        Task<LanguageSample?> GetLanguageSampleAsync(int id);
        void UpdateLanguageSample(LanguageSample updated);
        Task<LanguageSample> CreateLanguageSampleAsync(LanguageSample sample);
        Task RemoveLanguageSampleAsync(int sampleId);

        Task<HiDimBipolarVector?> GetLanguageVectorFromCacheAsync(LanguageSample languageSample, Alphabet alphabet);
        Task AddLanguageVectorToCacheAsync(LanguageSample languageSample, Alphabet alphabet, HiDimBipolarVector vector);
        Task RemoveMultilingualSampleFromCacheAsync(int multilingualSampleId);
        void RemoveLanguageSampleFromCache(int languageSampleId);
        void RemoveAlphabetFromCache(int alphabetId);

        Task Save();
    }
}
