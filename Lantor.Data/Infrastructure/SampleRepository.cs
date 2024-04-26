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

        public Alphabet GetAlphabet(int id)
        {
            return context.Alphabets.AsNoTracking().First(a => a.Id == id);
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

        public async Task UpdateLanguageSample(LanguageSample updated)
        {
            context.LanguageSamples.Update(updated);
            await context.SaveChangesAsync();
        }

        public async Task<LanguageSample> CreateLanguageSample(LanguageSample sample)
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

        public IList<Alphabet> GetAllAlphabets()
        {
            return context.Alphabets.AsNoTracking().ToList();
        }

        public async Task<Alphabet> CreateAlphabet(string name, int dim)
        {
            var abc = new Alphabet(name, dim, new RandomVectorFactory());
            var added = context.Alphabets.Add(abc);
            await context.SaveChangesAsync();
            return added.Entity;
        }
    }
}
