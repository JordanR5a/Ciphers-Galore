using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ciphers_Galore.Model
{
    public class Skip : Cipher
    {
        //TODO List answers by method to show work
        public override List<string> Decrypt(string message, bool showSteps)
        {
            var words = new string(message.Where(c => Char.IsLetter(c) || Char.IsWhiteSpace(c)).ToArray()).Split(" ").Where(word => !word.Equals("")).ToArray();
            message = new string(message.Where(c => Char.IsLetter(c)).ToArray());
            var possibleAnswers = new List<string>();

            int max = words.OrderBy(w => w.Length).First().Length;
            Parallel.For(0, max, i => 
            {
                var answer = LetterPosition(words, i);
                if (answer != null) possibleAnswers.Add(answer);
            });
            if (showSteps) Console.WriteLine("Checking by letter position...");

            possibleAnswers.Add(LastLetter(words));
            if (showSteps) Console.WriteLine("Checking by last letter...");

            int variations = message.Length / 4;
            Parallel.For(2, variations, i =>
            {
                for (int k = 0; k < i; k++) possibleAnswers.Add(EveryNthLetter(message, i, k));
            });
            if (showSteps) Console.WriteLine("Checking every nth position...");

            possibleAnswers.Add(Staircase(words));
            if (showSteps) Console.WriteLine("Checking by staircase...");

            var realWordAnswers = new List<string>();
            foreach (var op in possibleAnswers) realWordAnswers.AddRange(FindPossibleAnswers(op.ToLower()));

            return realWordAnswers;
        }

        private string LetterPosition(string[] words, int index)
        {
            if (words.OrderBy(w => w.Length).First().Length < index) return null;

            var answer = new StringBuilder();
            for (int word = 0; word < words.Length; word++)
            {
                answer.Append(words[word][index]);
            }
            return answer.ToString();
        }

        private string LastLetter(string[] words)
        {
            var answer = new StringBuilder();
            for (int index = 0; index < words.Length; index++)
            {
                var word = words[index];
                answer.Append(word[word.Length - 1]);
            }
            return answer.ToString();
        }

        private string Staircase(string[] words)
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

            return answer.ToString();
        }

        private string EveryNthLetter(string message, int n, int offset)
        {
            var answer = new StringBuilder();

            int let = 1 + offset;
            foreach (var c in message)
            {
                if (let % n == 0) answer.Append(c);
                let++;
            }

            return answer.ToString();
        }
    }
}
