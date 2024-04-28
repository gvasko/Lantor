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
    public class SampleRepository : ISampleRepository
    {
        private readonly LantorContext context;

        public SampleRepository(LantorContext context) 
        {
            this.context = context;
        }

        public async Task<Alphabet> GetDefaultAlphabetAsync()
        {
            return await context.Alphabets.AsNoTracking().FirstAsync(a => a.Name == "Default");
        }

        public async Task<Alphabet?> GetAlphabetAsync(int id)
        {
            return await context.Alphabets.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<MultilingualSample> GetDefaultSamplesAsync()
        {
            return await context.MultilingualSamples.AsNoTracking().Include(ms => ms.Languages).FirstAsync(s => s.Name == "Default");
        }

        public Task<HiDimBipolarVector?> GetLanguageVectorFromCacheAsync(LanguageSample languageSample, Alphabet alphabet)
        {
            var t = new Task<HiDimBipolarVector?>(() =>
            {
                var result = context.LanguageVectorCache.AsNoTracking().FirstOrDefaultAsync(c =>
                    c.LanguageSampleId == languageSample.Id && c.AlphabetId == alphabet.Id);
                
                result.Wait();

                return result.Result?.Vector;
            });
            t.Start();
            return t;
        }

        public async Task AddLanguageVectorToCacheAsync(LanguageSample languageSample, Alphabet alphabet, HiDimBipolarVector vector)
        {
            context.LanguageVectorCache.Add(new LanguageVectorCache(languageSample.Id, alphabet.Id, vector));
            await context.SaveChangesAsync();
        }

        public async Task<IList<MultilingualSample>> GetAllMultilingualSamplesAsync()
        {
            return await context.MultilingualSamples.AsNoTracking().Include(mls => mls.Languages).ToListAsync();
        }

        public async Task<MultilingualSample?> GetMultilingualSampleAsync(int id)
        {
            return await context.MultilingualSamples.AsNoTracking().Include(mls => mls.Languages).Where(mls => mls.Id == id).FirstOrDefaultAsync();
        }

        public async Task<LanguageSample?> GetLanguageSampleAsync(int id)
        {
            return await context.LanguageSamples.AsNoTracking().Where(ls => ls.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateMultilingualSampleAsync(MultilingualSample updated)
        {
            // Do not update related objects
            updated.Languages = [];
            context.MultilingualSamples.Update(updated);
            await context.SaveChangesAsync();
        }

        public async Task<MultilingualSample> CreateMultilingualSampleAsync(MultilingualSample sample)
        {
            sample.Id = 0;
            sample.Languages = [];
            var added = context.MultilingualSamples.Add(sample);
            await context.SaveChangesAsync();
            return added.Entity;
        }

        public async Task UpdateLanguageSampleAsync(LanguageSample updated)
        {
            context.LanguageSamples.Update(updated);
            await context.SaveChangesAsync();
        }

        public async Task<LanguageSample> CreateLanguageSampleAsync(LanguageSample sample)
        {
            if (sample.MultilingualSampleId == 0)
            {
                throw new Exception("Invalid language sample: no parent specified");
            }
            sample.Id = 0;
            var added = context.Add(sample);
            await context.SaveChangesAsync();
            return added.Entity;
        }

        public async Task<IList<Alphabet>> GetAllAlphabetsAsync()
        {
            return await context.Alphabets.AsNoTracking().ToListAsync();
        }

        public async Task<Alphabet> CreateAlphabetAsync(string name, int dim)
        {
            var abc = new Alphabet(name, dim, new RandomVectorFactory());
            var added = context.Alphabets.Add(abc);
            await context.SaveChangesAsync();
            return added.Entity;
        }
    }
}
