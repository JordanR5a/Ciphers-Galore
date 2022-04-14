using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Ciphers_Galore.Model
{
    public class Affine : Cipher
    {
        public char[] GetConversion(int multiplierKey, int additiveKey)
        {
            var answer = new char[Alphabet.Length];
            for (int let = 0; let < Alphabet.Length; let++)
            {
                int index = ((Alphabet.ToList().IndexOf(Alphabet[let]) * multiplierKey) + additiveKey) % Alphabet.Length;
                while (index < 0) index += Alphabet.Length;

                answer[let] = Alphabet[index];
            }
            return answer;
        }

        public override List<string> Decrypt(string message, bool showSteps)
        {
            message = new string(message.Where(c => Char.IsLetter(c)).ToArray()).ToLower();

            var answers = new List<string>();
            for (int multiplicativeKey = 1; multiplicativeKey < Alphabet.Length; multiplicativeKey++)
            {
                for (int additiveKey = 0; additiveKey < Alphabet.Length; additiveKey++)
                {
                    var conversion = GetConversion(multiplicativeKey, additiveKey);
                    if (!Alphabet.All(l => conversion.Contains(l))) continue;

                    var answer = new StringBuilder();
                    foreach (var let in message)
                    {

                        var tmp = conversion.ToList().IndexOf(let);
                        answer.Append(Alphabet[conversion.ToList().IndexOf(let)]);
                    }

                    if (showSteps)
                    {
                        Console.WriteLine("Multiplicative key of " + multiplicativeKey + "; with additive " + additiveKey + "; Affine key is " + ((multiplicativeKey * Alphabet.Length) + additiveKey) + ": " + answer.ToString());
                        Console.WriteLine("Cipher: " + new string(conversion) + " => " + new string(Alphabet));
                        Console.WriteLine();
                    }

                    answers.Add(answer.ToString());
                }
            }

            var realWordAnswers = new List<string>();
            foreach (var op in answers) realWordAnswers.AddRange(FindPossibleRealWordAnswers(op.ToLower()));

            return realWordAnswers;
        }

        public string Encrypt(string message, int multiplicativeKey, int additiveKey, bool showSteps)
        {
            message = new string(message.Where(c => Char.IsLetter(c)).ToArray()).ToLower();

            var answer = new StringBuilder();
            foreach (var let in message)
            {
                int index = ((Alphabet.ToList().IndexOf(let) * multiplicativeKey) + additiveKey) % Alphabet.Length;
                answer.Append(Alphabet[index]);
            }

            if (showSteps) Console.WriteLine("Affine Key: " + ((multiplicativeKey * Alphabet.Length) + additiveKey));
            return answer.ToString();
        }

        public string Encrypt(string message, int affineKey, bool showSteps)
        {
            message = new string(message.Where(c => Char.IsLetter(c)).ToArray()).ToLower();
            int multiplicativeKey = (int)(affineKey / Alphabet.Length);
            int additiveKey = affineKey % Alphabet.Length;

            var answer = new StringBuilder();
            foreach (var let in message)
            {
                int index = ((Alphabet.ToList().IndexOf(let) * multiplicativeKey) + additiveKey) % Alphabet.Length;
                answer.Append(Alphabet[index]);
            }

            if (showSteps)
            {
                Console.WriteLine("Mltiplicative Key: " + multiplicativeKey);
                Console.WriteLine("Additive Key: " + additiveKey);
            }
            return answer.ToString().ToUpper();
        }
    }
}
