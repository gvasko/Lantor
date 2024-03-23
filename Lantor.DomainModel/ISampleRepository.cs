using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lantor.DomainModel
{
    public interface ISampleRepository
    {
        MultilingualSample GetDefaultSamples();
        //Alphabet GetDefaultAlphabet();
        //HiDimBipolarVector GetLanguageVector(LanguageSample languageSample, Alphabet alphabet);
    }
}
