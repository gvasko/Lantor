using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lantor.DomainModel
{
    public class DomainUnitOfWork : IDomainUnitOfWork
    {
        private IBasicCrudUnitOfWork _basicUow;

        public DomainUnitOfWork(IBasicCrudUnitOfWork uow)
        {
            _basicUow = uow;
        }

        public IBasicCrudUnitOfWork BasicCrudOperations { get => _basicUow; }

        public async Task<Alphabet> CreateAlphabetAsync(string name, int dim)
        {
            var abc = new Alphabet(name, dim, new RandomVectorFactory());
            return await _basicUow.CreateAlphabetAsync(abc);
        }

        public async Task<Alphabet> GetDefaultAlphabetAsync()
        {
            var allAbc = await _basicUow.GetAllAlphabetListInfoAsync();
            var defaultShallowAbc = allAbc.First(a => a.Name == "Default");
            var defaultAbc = await _basicUow.GetAlphabetAsync(defaultShallowAbc.Id);

            if (defaultAbc == null)
            {
                throw new InvalidOperationException("Default alphabet not found");
            }

            return defaultAbc;
        }

        public async Task RemoveAlphabetAsync(int alphabetId)
        {
            // EF Core will cascade delete
            //_basicUow.RemoveAlphabetFromCache(alphabetId);
            await _basicUow.RemoveAlphabetAsync(alphabetId);
        }

        public async Task<MultilingualSample> GetDefaultSamplesAsync()
        {
            var allSamples = await _basicUow.GetAllMultilingualSamplesAsync();
            var defaultSamples = allSamples.First(s => s.Name == "Default");
            return defaultSamples;
        }

        public async Task RemoveMultilingualSampleAsync(int multilingualSampleId)
        {
            // EF Core will cascade delete
            //await _basicUow.RemoveMultilingualSampleFromCacheAsync(multilingualSampleId);
            await _basicUow.RemoveCascadeMultilingualSampleAsync(multilingualSampleId);
        }

        public async Task RemoveLanguageSampleAsync(int sampleId)
        {
            // EF Core will cascade delete
            //_basicUow.RemoveLanguageSampleFromCache(sampleId);
            await _basicUow.RemoveLanguageSampleAsync(sampleId);
        }

        public async Task Save()
        {
            await _basicUow.Save();
        }

        public void UpdateLanguageSample(LanguageSample updated)
        {
            _basicUow.UpdateLanguageSample(updated);
            _basicUow.RemoveLanguageSampleFromCache(updated.Id);
        }

    }
}
