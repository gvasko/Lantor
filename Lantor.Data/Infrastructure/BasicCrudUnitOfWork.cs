using Lantor.DomainModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lantor.Data.Infrastructure
{
    /// <summary>
    /// also UOW
    /// </summary>
    public class BasicCrudUnitOfWork : IBasicCrudUnitOfWork
    {
        private readonly LantorContext context;

        public BasicCrudUnitOfWork(LantorContext context) 
        {
            this.context = context;
        }

        public async Task<Alphabet?> GetAlphabetAsync(int id)
        {
            return await context.Alphabets.AsNoTracking().Include(a => a.LetterVectors).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IList<Alphabet>> GetAllAlphabetListInfoAsync()
        {
            return await context.Alphabets.AsNoTracking().ToListAsync();
        }

        public async Task<Alphabet> CreateAlphabetAsync(Alphabet alphabet)
        {
            var added = await context.Alphabets.AddAsync(alphabet);
            return added.Entity;
        }

        public async Task RemoveAlphabetAsync(int alphabetId)
        {
            var alphabet = await GetAlphabetAsync(alphabetId);
            if (alphabet == null)
            {
                throw new ArgumentException($"Unable to delete, Alphabet not found with ID {alphabetId}");
            }

            context.Alphabets.Remove(alphabet);
        }

        public async Task<IList<MultilingualSample>> GetAllMultilingualSamplesAsync()
        {
            // TODO: listinfo
            return await context.MultilingualSamples.AsNoTracking().Include(mls => mls.Languages).ToListAsync();
        }

        public async Task<MultilingualSample?> GetMultilingualSampleAsync(int id)
        {
            return await context.MultilingualSamples.AsNoTracking().Include(mls => mls.Languages).Where(mls => mls.Id == id).FirstOrDefaultAsync();
        }

        public void UpdateMultilingualSample(MultilingualSample updated)
        {
            // Do not update related objects
            updated.Languages = [];
            context.MultilingualSamples.Update(updated);
        }

        public async Task<MultilingualSample> CreateMultilingualSampleAsync(MultilingualSample sample)
        {
            sample.Id = 0;
            sample.Languages = [];
            var added = await context.MultilingualSamples.AddAsync(sample);
            return added.Entity;
        }

        public async Task RemoveCascadeMultilingualSampleAsync(int multilingualSampleId)
        {
            var mlSample = await GetMultilingualSampleAsync(multilingualSampleId);
            if (mlSample == null)
            {
                throw new ArgumentException($"Unable to delete, MultilingualSample not found with ID {multilingualSampleId}");
            }

            context.MultilingualSamples.Remove(mlSample);
        }

        public async Task<LanguageSample?> GetLanguageSampleAsync(int id)
        {
            return await context.LanguageSamples.AsNoTracking().Where(ls => ls.Id == id).FirstOrDefaultAsync();
        }

        public void UpdateLanguageSample(LanguageSample updated)
        {
            context.LanguageSamples.Update(updated);
        }

        public async Task<LanguageSample> CreateLanguageSampleAsync(LanguageSample sample)
        {
            if (sample.MultilingualSampleId == 0)
            {
                throw new Exception("Invalid language sample: no parent specified");
            }
            sample.Id = 0;
            var added = await context.AddAsync(sample);
            return added.Entity;
        }

        public async Task RemoveLanguageSampleAsync(int sampleId)
        {
            var ls = await GetLanguageSampleAsync(sampleId);
            if (ls == null)
            {
                return;
            }

            context.LanguageSamples.Remove(ls);
        }

        public async Task RemoveMultilingualSampleFromCacheAsync(int multilingualSampleId)
        {
            var mlSample = await GetMultilingualSampleAsync(multilingualSampleId);
            if (mlSample == null)
            {
                return;
            }

            foreach (var ls in mlSample.Languages)
            {
                RemoveLanguageSampleFromCache(ls.Id);
            }
        }

        public void RemoveLanguageSampleFromCache(int languageSampleId)
        {
            var removeFromCache = context.LanguageVectorCache.Where(lv => lv.LanguageSampleId == languageSampleId);
            foreach (var entry in removeFromCache)
            {
                context.LanguageVectorCache.Remove(entry);
            }
        }

        public void RemoveAlphabetFromCache(int alphabetId)
        {
            var removeFromCache = context.LanguageVectorCache.Where(lv => lv.AlphabetId == alphabetId);
            foreach (var entry in removeFromCache)
            {
                context.LanguageVectorCache.Remove(entry);
            }
        }

        public async Task<HiDimBipolarVector?> GetLanguageVectorFromCacheAsync(LanguageSample languageSample, Alphabet alphabet)
        {
            return (await context.LanguageVectorCache.AsNoTracking().FirstOrDefaultAsync(c =>
                c.LanguageSampleId == languageSample.Id && c.AlphabetId == alphabet.Id))?.Vector;
        }

        public async Task AddLanguageVectorToCacheAsync(LanguageSample languageSample, Alphabet alphabet, HiDimBipolarVector vector)
        {
            await context.LanguageVectorCache.AddAsync(new LanguageVectorCache(languageSample.Id, alphabet.Id, vector));
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
