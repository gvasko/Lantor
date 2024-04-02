using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lantor.DomainModel
{
    internal interface ILanguageVectorBuilder
    {
        HiDimBipolarVector BuildLanguageVector(string sample);
    }
}
