using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ciphers_Galore.Model
{
    class Caesar : Cipher
    {
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

                if (showSteps) Console.WriteLine("Shift of " + shift + ": " + answer.ToString());
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
