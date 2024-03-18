using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lantor.DomainModel.Compute
{
    internal class Alphabet
    {
        private NaiveHiDimBipolarVector? SpaceVector { get; set; }

        private NaiveHiDimBipolarVector[]? AlphabetVectors { get; set; }

        public Alphabet()
        {

        }

        public Alphabet(int dim)
        {
            SpaceVector = NaiveHiDimBipolarVector.CreateRandomVector(dim);
            AlphabetVectors = new NaiveHiDimBipolarVector[26];
            for (int i = 0; i < AlphabetVectors.Length; i++)
            {
                AlphabetVectors[i] = NaiveHiDimBipolarVector.CreateRandomVector(dim);
            }
        }

        public int Dim
        {
            get
            {
                return SpaceVector == null ? 0 : SpaceVector.Length;
            }
        }

        public NaiveHiDimBipolarVector GetVectorForLetter(char letter)
        {
            if (Char.IsWhiteSpace(letter))
            {
                return SpaceVector;
            }
            else if (char.IsLetter(letter))
            {
                var index = char.ToUpper(letter) - 'A';
                return AlphabetVectors[index];
            }

            throw new ArgumentException($"Not a letter or a whitespace: {letter}");
        }

    }
}
