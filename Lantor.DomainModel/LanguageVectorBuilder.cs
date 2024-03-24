﻿using System;
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
            var lastFix = whitespacesRegex.Replace(only26str, " ");
            lastFix = lastFix.ToLower();

            // German
            var ssRegex = new Regex(@"\u00DF");
            lastFix = ssRegex.Replace(lastFix, "ss");

            // Polish
            var lStrokeRegex = new Regex(@"\u0141|\u0142");
            lastFix = lStrokeRegex.Replace(lastFix, "l");

            // French
            var oeRegex = new Regex(@"\u0152|\u0153|\u0276");
            lastFix = oeRegex.Replace(lastFix, "oe");

            var illegalRegex = new Regex("[^ a-z]");
            var illegalChars = illegalRegex.Matches(lastFix);
            if (illegalChars.Count > 0)
            {
                var illegals = new StringBuilder();
                foreach (var illegalChar in illegalChars)
                {
                    illegals.Append(illegalChar);
                }
                Console.WriteLine($"WARNING! Illegal characters found: {illegals}");
                lastFix = illegalRegex.Replace(lastFix, " ");
            }
            return lastFix;
        }

    }
}
