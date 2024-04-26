using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lantor.DomainModel
{
    public interface ILanguageVectorBuilder
    {
        HiDimBipolarVector BuildLanguageVector(Alphabet alphabet, string sample);
    }
}
