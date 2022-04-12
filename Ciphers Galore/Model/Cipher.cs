using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ciphers_Galore.Model
{
    public class Cipher
    {
        protected static readonly char[] Alphabet = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        protected static Library Library;

        public Cipher()
        {
            Library = new Library();
        }

        public virtual List<string> Decrypt(string message, bool showSteps) { return null; }

        public virtual string Encrypt(string message, bool showSteps) { return null; }

        protected static List<string> FindPossibleRealWordAnswers(string rawText)
        {
            return IFindPossibleRealWordAnswers("", rawText);
        }

        //TODO: Instead of only accepting real world answers, find results with the MOST real words in it with the least amount of remaining letters
        private static List<string> IFindPossibleRealWordAnswers(string resultText, string rawText)
        {
            List<string> answers = new List<string>();
            if (rawText.Equals(""))
                answers.Add(resultText);
            else if (resultText.Trim().Split(" ").Length > 2)
                answers.Add(resultText + rawText);

            int max = Library.LargestWordLength > rawText.Length ? rawText.Length : Library.LargestWordLength;

            if (rawText.Length > 25)
            {
                //Console.WriteLine("Accelerating search... " + resultText);
                var paths = new List<string>();
                for (int i = 1; i < max; i++)
                    if (Library.IsRealWord(rawText.Substring(0, i)))
                        paths.Add(rawText.Substring(0, i) + "," + i);

                if (paths.Count != 0)
                {
                    var parts = paths.OrderByDescending(p => p.Length).First().Split(",");
                    answers.AddRange(IFindPossibleRealWordAnswers(resultText + parts[0] + " ", rawText.Substring(int.Parse(parts[1]))));
                }

            }
            else
            {
                //Console.WriteLine("Glandular search... " + resultText);
                Parallel.For(1, max + 1, i =>
                {
                    if (Library.IsRealWord(rawText.Substring(0, i)))
                        answers.AddRange(IFindPossibleRealWordAnswers(resultText + rawText.Substring(0, i) + " ", rawText.Substring(i)));
                });
            }
            return answers;
        }

        //https://stackoverflow.com/questions/24094093/how-to-print-2d-array-to-console-in-c-sharp
        protected static void Print2DArray<T>(T[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        //https://stackoverflow.com/questions/15743192/check-if-number-is-prime-number
        protected static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
                if (number % i == 0)
                    return false;

            return true;
        }

        //https://stackoverflow.com/questions/7483706/c-sharp-modinverse-function
        protected static int ModInverse(int a, int n)
        {
            int i = n, v = 0, d = 1;
            while (a > 0)
            {
                int t = i / a, x = a;
                a = i % x;
                i = x;
                x = d;
                d = v - t * x;
                v = x;
            }
            v %= n;
            if (v < 0) v = (v + n) % n;
            return v;
        }

    }
}
