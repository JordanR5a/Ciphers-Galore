using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Ciphers_Galore.Model
{
    public class AnswerComparer : IComparer<string>
    {
        public int Compare([AllowNull] string x, [AllowNull] string y)
        {
            if (x == null || y == null) return 0;

            var xWords = x.Split(" ").OrderByDescending(w => w.Length).ToArray();
            var yWords = y.Split(" ").OrderByDescending(w => w.Length).ToArray();

            int max = xWords.Length > yWords.Length ? yWords.Length : xWords.Length;
            for (int i = 0; i < max; i++)
            {
                if (xWords[i].Length > yWords[i].Length) return 1;
                else if (xWords[i].Length < yWords[i].Length) return -1;
            }
            return 0;
        }
    }
}
