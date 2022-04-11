using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Ciphers_Galore.Model
{
    class Multiplicative : Cipher
    {
        public char[] GetConversion(int key)
        {
            var answer = new char[Alphabet.Length];
            for (int let = 0; let < Alphabet.Length; let++)
            {
                int index = (Alphabet.ToList().IndexOf(Alphabet[let]) * key) % Alphabet.Length;
                while (index < 0) index += Alphabet.Length;

                answer[let] = Alphabet[index];
            }
            return answer;
        }

        public override List<string> Decrypt(string message, bool showSteps)
        {
            message = new string(message.Where(c => Char.IsLetter(c)).ToArray()).ToLower();

            var answers = new List<string>();
            for (int key = 0; key < Alphabet.Length; key++)
            {
                var conversion = GetConversion(key);
                if (!Alphabet.All(l => conversion.Contains(l))) continue;

                var answer = new StringBuilder();
                foreach (var let in message)
                {

                    var tmp = conversion.ToList().IndexOf(let);
                    answer.Append(Alphabet[conversion.ToList().IndexOf(let)]);
                }

                if (showSteps)
                {
                    Console.WriteLine("Key of " + key + ": " + answer.ToString());
                    Console.WriteLine("Plain Text: " + new string(Alphabet));
                    Console.WriteLine("Conversion: " + new string(conversion));
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
                int index = (Alphabet.ToList().IndexOf(let) * key) % Alphabet.Length;
                answer.Append(Alphabet[index]);
            }

            return answer.ToString();
        }
    }
}
