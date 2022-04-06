using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ciphers_Galore.Model
{
    public class Cipher
    {
        protected static Library Library;

        public Cipher()
        {
            Library = new Library();
        }

        public virtual List<string> Decrypt(string message, bool showSteps) { return null; }

        public virtual string Encrypt(string message, bool showSteps) { return null; }

        protected static List<string> FindPossibleAnswers(string rawText)
        {
            return IFindPossibleAnswers("", rawText);
        }

        //TODO: Instead of branching, only go with the largest match to optimize runtime
        private static List<string> IFindPossibleAnswers(string resultText, string rawText)
        {
            //Console.WriteLine(resultText);
            List<string> answers = new List<string>();
            if (rawText.Equals(""))
                answers.Add(resultText);

            int max = Library.LargestWordLength > rawText.Length ? rawText.Length : Library.LargestWordLength;
            Parallel.For(1, max + 1, i =>
            {
                if (Library.IsRealWord(rawText.Substring(0, i)))
                    answers.AddRange(IFindPossibleAnswers(resultText + rawText.Substring(0, i) + " ", rawText.Substring(i)));
            });
            return answers;
        }

        //https://stackoverflow.com/questions/24094093/how-to-print-2d-array-to-console-in-c-sharp
        public static void Print2DArray<T>(T[,] matrix)
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

    }
}
