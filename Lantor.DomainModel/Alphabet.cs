using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lantor.DomainModel
{
    public class Alphabet
    {
        public int Id { get; set; }
        public List<LetterVector> LetterVectors { get; init; }

        public Alphabet()
        {
            LetterVectors = [];
        }

        public Alphabet(int dim)
        {
            LetterVectors = new List<LetterVector>();
            LetterVectors.Add(new LetterVector(' ', HiDimBipolarVector.CreateRandomVector(dim)));
            for (char c = 'a'; c <= 'z'; c++)
            {
                LetterVectors.Add(new LetterVector(c, HiDimBipolarVector.CreateRandomVector(dim)));
            }
        }

        public int Dim
        {
            get
            {
                return LetterVectors.Count == 0 ? 0 : LetterVectors[0].Vector.Length;
            }
        }

        public HiDimBipolarVector GetVectorForLetter(char letter)
        {
            return LetterVectors.First(x => x.Letter == letter).Vector;
        }
    }
}
