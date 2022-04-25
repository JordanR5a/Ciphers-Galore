using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ciphers_Galore.Model
{
    public class Caesar : Cipher
    {
        private char[] GetConversion(int shift)
        {
            var answer = new char[Alphabet.Length];
            for (int let = 0; let < Alphabet.Length; let++)
            {
                int index = Alphabet.ToList().IndexOf(Alphabet[let]) - shift;
                while (index < 0) index += Alphabet.Length;

                answer[let] = Alphabet[index];
            }
            return answer;
        }

        private char[] GetConversion(string keyword)
        {
            var conversion = new List<char>(keyword.ToCharArray().Distinct());
            foreach (var let in Alphabet)
                if (!conversion.Contains(let)) conversion.Add(let);
            return conversion.ToArray();
        }

        public List<string> Decrypt(string message, string keyword, bool showSteps)
        {
            message = new string(message.Where(c => Char.IsLetter(c)).ToArray()).ToLower();
            keyword = new string(keyword.Where(c => Char.IsLetter(c)).ToArray()).ToLower();

            var conversion = GetConversion(keyword);

            var answer = new StringBuilder();
            foreach (var let in message)
            {
                var tmp = conversion.ToList().IndexOf(let);
                answer.Append(Alphabet[conversion.ToList().IndexOf(let)]);
            }

            if (showSteps)
            {
                Console.WriteLine("Answer: " + answer.ToString());
                Console.WriteLine("Cipher: " + new string(conversion) + " => " + new string(Alphabet));
                Console.WriteLine();
            }

            return FindPossibleRealWordAnswers(answer.ToString());
        }

        public override List<string> Decrypt(string message, bool showSteps)
        {
            message = new string(message.Where(c => Char.IsLetter(c)).ToArray()).ToLower();

            var answers = new List<string>();
            for (int shift = 1; shift < Alphabet.Length; shift++)
            {
                var answer = new StringBuilder();
                foreach (var let in message)
                {
                    int index = Alphabet.ToList().IndexOf(let) - shift;
                    while (index < 0) index += Alphabet.Length;

                    answer.Append(Alphabet[index]);
                }

                if (showSteps)
                {
                    Console.WriteLine("Shift of " + Math.Abs(shift - Alphabet.Length) + ": " + answer.ToString());
                    Console.WriteLine("Cipher: " + new string(Alphabet) + " => " + new string(GetConversion(shift)));
                    Console.WriteLine();
                }
                answers.Add(answer.ToString());
            }

            var realWordAnswers = new List<string>();
            foreach (var op in answers) realWordAnswers.AddRange(FindPossibleRealWordAnswers(op.ToLower()));

            return realWordAnswers;
        }

        public string Encrypt(string message, int key, bool showSteps)
        {
            message = new string(message.Where(c => Char.IsLetter(c)).ToArray()).ToLower();

            var answer = new StringBuilder();
            foreach (var let in message)
            {
                int index = Alphabet.ToList().IndexOf(let) + key;
                while (index >= Alphabet.Length) index -= Alphabet.Length;

                answer.Append(Alphabet[index]);
            }

            return answer.ToString().ToUpper();
        }
    }
}
