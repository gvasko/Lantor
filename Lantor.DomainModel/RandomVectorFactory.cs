using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lantor.DomainModel
{
    internal class RandomVectorFactory : IVectorFactory
    {
        public HiDimBipolarVector CreateLetterVectorFor(int dim, char letter)
        {
            return HiDimBipolarVector.CreateRandomVector(dim);
        }
    }
}
