using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lantor.DomainModel
{
    public interface IDomainUnitOfWork
    {
        public IBasicCrudUnitOfWork BasicCrudOperations { get; }
        Task<Alphabet> CreateAlphabetAsync(string name, int dim);
        Task<Alphabet> GetDefaultAlphabetAsync();
        Task RemoveAlphabetAsync(int alphabetId);
        Task<MultilingualSample> GetDefaultSamplesAsync();
        Task RemoveMultilingualSampleAsync(int multilingualSampleId);
        void UpdateLanguageSample(LanguageSample updated);
        Task RemoveLanguageSampleAsync(int sampleId);

        Task<User> EnsureCurrentUserAsync(User user);
        Task<User> ConstructCurrentUserAsync(User user);
        User GetCurrentUser();

        Task Save();
    }
}
