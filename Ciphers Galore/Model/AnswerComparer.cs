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

            var xWords = x.Split(" ");
            var yWords = y.Split(" ");

            var xValues = xWords.Take(xWords.Length - 1).OrderByDescending(w => w.Length).ToArray();
            var yValues = yWords.Take(yWords.Length - 1).OrderByDescending(w => w.Length).ToArray();


            int max = xValues.Length > yValues.Length ? yValues.Length : xValues.Length;
            for (int i = 0; i < max; i++)
            {
                if (xValues[i].Length > yValues[i].Length) return 1;
                else if (xValues[i].Length < yValues[i].Length) return -1;
            }
            return 0;
        }
    }
}
