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

        public Alphabet GetDefaultAlphabet()
        {
            return context.Alphabets.AsNoTracking().First(a => a.Name == "Default");
        }

        public MultilingualSample GetDefaultSamples()
        {
            return context.MultilingualSamples.AsNoTracking().Include(ms => ms.Languages).First(s => s.Name == "Default");
        }

        public HiDimBipolarVector? GetLanguageVectorFromCache(LanguageSample languageSample, Alphabet alphabet)
        {
            return context.LanguageVectorCache.AsNoTracking().FirstOrDefault(c => 
                c.LanguageSampleId == languageSample.Id && c.AlphabetId == alphabet.Id)?.Vector;
        }

        public void AddLanguageVectorToCache(LanguageSample languageSample, Alphabet alphabet, HiDimBipolarVector vector)
        {
            context.LanguageVectorCache.Add(new LanguageVectorCache(languageSample.Id, alphabet.Id, vector));
            context.SaveChanges();
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

        public async Task UpdateMultilingualSample(MultilingualSample updated)
        {
            // Do not update related objects
            updated.Languages = [];
            context.MultilingualSamples.Update(updated);
            await context.SaveChangesAsync();
        }

        public async Task<MultilingualSample> CreateMultilingualSample(MultilingualSample sample)
        {
            sample.Id = 0;
            sample.Languages = [];
            var added = context.MultilingualSamples.Add(sample);
            await context.SaveChangesAsync();
            return added.Entity;
        }
    }
}
