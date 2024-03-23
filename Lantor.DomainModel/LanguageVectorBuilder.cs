using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lantor.DomainModel
{
    internal class LanguageVectorBuilder
    {
        private readonly Alphabet _alphabet;

        public LanguageVectorBuilder(Alphabet alphabet)
        {
            _alphabet = alphabet;
        }

        public HiDimBipolarVector BuildLanguageVector(string sample)
        {
            string asciiSample = Normalize(sample);

            if (asciiSample.Length < 3)
            {
                throw new ArgumentException("Too short sample");
            }

            var steps = asciiSample.Length - 2;

            var sumBuilder = new HiDimBipolarSumBuilder(_alphabet.Dim);

            for (int i = 0; i < steps; i++)
            {
                var tri = asciiSample.Substring(i, 3);
                var c0 = _alphabet.GetVectorForLetter(tri[0]);
                var c1 = _alphabet.GetVectorForLetter(tri[1]);
                var c2 = _alphabet.GetVectorForLetter(tri[2]);
                var pc0 = c0.Permute().Permute();
                var pc1 = c1.Permute();
                var pc2 = c2;
                var trigram = pc0.Multiply(pc1).Multiply(pc2);

                sumBuilder.Add(trigram);
            }

            return sumBuilder.BuildVector();
        }

        private static string Normalize(string sample)
        {
            var temp = sample.Normalize(NormalizationForm.FormD);
            var only26str = string.Concat(temp.Where(
                c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark &&
                (char.IsLetter(c) || char.IsWhiteSpace(c))));
            var whitespacesRegex = new Regex(@"\s+");
            var singleWSp = whitespacesRegex.Replace(only26str, " ");

            // German
            var ssRegex = new Regex(@"\u00DF");
            var noSS = ssRegex.Replace(singleWSp, "ss");

            // Polish
            var lStrokeRegex1 = new Regex(@"\u0141");
            var noLStroke1 = lStrokeRegex1.Replace(noSS, "L");
            var lStrokeRegex2 = new Regex(@"\u0142");
            var noLStroke2 = lStrokeRegex2.Replace(noLStroke1, "l");

            return noLStroke2;
        }

    }
}
