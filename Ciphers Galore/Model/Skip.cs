using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ciphers_Galore.Model
{
    public class Skip : Cipher
    {
        public override List<string> Decrypt(string message, bool showSteps)
        {
            var words = new string(message.Where(c => Char.IsLetter(c) || Char.IsWhiteSpace(c)).ToArray()).Split(" ").Where(word => !word.Equals("")).ToArray();
            message = new string(message.Where(c => Char.IsLetter(c)).ToArray());
            var possibleAnswers = new List<string>();

            int max = words.OrderBy(w => w.Length).First().Length;
            for (int i = 0; i < max; i++)
            {
                if (showSteps) Console.Write("Checking letter position " + (i + 1) + "...");
                var answer = LetterPosition(words, i, showSteps);
                if (answer != null) possibleAnswers.Add(answer);
            }

            if (showSteps) Console.Write("Checking by last letter...");
            possibleAnswers.Add(LastLetter(words, showSteps));

            int variations = message.Length / 4;
            for (int i = 2; i < variations; i++)
            {
                for (int k = 0; k < i; k++)
                {
                    if (showSteps) Console.Write("Checking position " + i + ", offset of " + k + "...");
                    possibleAnswers.Add(EveryNthLetter(message, i, k, showSteps));
                }
            }

            if (showSteps) Console.Write("Checking by staircase...");
            possibleAnswers.Add(Staircase(words, showSteps));

            var realWordAnswers = new List<string>();
            foreach (var op in possibleAnswers) realWordAnswers.AddRange(FindPossibleAnswers(op.ToLower()));

            return realWordAnswers;
        }

        private string LetterPosition(string[] words, int index, bool showSteps)
        {
            if (words.OrderBy(w => w.Length).First().Length < index) return null;

            var answer = new StringBuilder();
            for (int word = 0; word < words.Length; word++)
            {
                answer.Append(words[word][index]);
            }
            if (showSteps) Console.WriteLine(answer);
            return answer.ToString();
        }

        private string LastLetter(string[] words, bool showSteps)
        {
            var answer = new StringBuilder();
            for (int index = 0; index < words.Length; index++)
            {
                var word = words[index];
                answer.Append(word[word.Length - 1]);
            }
            if (showSteps) Console.WriteLine(answer);
            return answer.ToString();
        }

        private string Staircase(string[] words, bool showSteps)
        {
            var answer = new StringBuilder();

            int step = 0;
            for (int i = 0; i < words.Length; i++)
            {
                var word = words[i];
                if (word.Length <= step) step = 0;

                answer.Append(word[step]);

                step++;
            }
            if (showSteps) Console.WriteLine(answer);
            return answer.ToString();
        }

        private string EveryNthLetter(string message, int n, int offset, bool showSteps)
        {
            var answer = new StringBuilder();

            int let = 1 + offset;
            foreach (var c in message)
            {
                if (let % n == 0) answer.Append(c);
                let++;
            }
            if (showSteps) Console.WriteLine(answer);
            return answer.ToString();
        }
    }
}
