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
            return context.Alphabets.First(a => a.Name == "Default");
        }

        public MultilingualSample GetDefaultSamples()
        {
            return context.MultilingualSamples.AsNoTracking().Include(ms => ms.Languages).First(s => s.Name == "Default");
        }

        public HiDimBipolarVector? GetLanguageVector(LanguageSample languageSample, Alphabet alphabet)
        {
            return context.LanguageVectorCache.AsNoTracking().FirstOrDefault(c => 
                c.LanguageSampleId == languageSample.Id && c.AlphabetId == alphabet.Id)?.Vector;
        }

        public void SetLanguageVector(LanguageSample languageSample, Alphabet alphabet, HiDimBipolarVector vector)
        {
            context.LanguageVectorCache.Add(new LanguageVectorCache(languageSample.Id, alphabet.Id, vector));
            context.SaveChanges();
        }
    }
}
