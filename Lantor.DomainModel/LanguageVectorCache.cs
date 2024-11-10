using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lantor.DomainModel
{
    public class LanguageVectorCache
    {
        public int Id { get; set; }
        public int? LanguageSampleId { get; set; }
        public LanguageSample? LanguageSample { get; set; }
        public int? AlphabetId { get; set; }
        public Alphabet? Alphabet { get; set; }
        public HiDimBipolarVector Vector { get; set; }

        public LanguageVectorCache()
        {
            Vector = new HiDimBipolarVector();
        }

        public LanguageVectorCache(int languageSampleId, int alphabetId, HiDimBipolarVector vector)
        {
            LanguageSampleId = languageSampleId;
            AlphabetId = alphabetId;
            Vector = vector;
        }
    }
}
