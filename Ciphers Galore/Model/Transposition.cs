using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ciphers_Galore.Model
{
    public class Transposition : Cipher
    {
        private static readonly char[] alphabet = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v' , 'w', 'x', 'y', 'z' };

        public List<string> Decrypt(string message, string keyword, bool showSteps)
        {
            message = new string(message.Where(c => Char.IsLetter(c)).ToArray()).ToLower();
            keyword = new string(keyword.ToLower());

            int size = (int)Math.Ceiling((double)message.Length / keyword.Length);

            char[] keywordTemplate = keyword.ToCharArray();
            char[,] grid = new char[size, keyword.Length];

            int buffer = (keyword.Length * size) - message.Length;
            for (int b = 0; b < buffer; b++) grid[size - 1, (keyword.Length - 1) - b] = '%';

            var stack = new Stack<char>(message.ToCharArray().Reverse().ToList());
            for (int let = 0; let < alphabet.Length; let++)
            {
                int position = keywordTemplate.ToList().IndexOf(alphabet[let]);
                if (position != -1)
                {
                    keywordTemplate[position] = '%';
                    let = -1;

                    for (int h = 0; h < size; h++)
                    {
                        if (grid[h, position] != '%') grid[h, position] = stack.Pop();
                    }
                }
            }

            var answer = new StringBuilder();
            for (int h = 0; h < size; h++)
            {
                for (int l = 0; l < keyword.Length; l++)
                {
                    if (grid[h, l] != '%') answer.Append(grid[h, l]);
                }
            }
            if (showSteps)
            {
                Console.WriteLine("Keyword Transposition (Key = " + keyword + "): " + answer);
                Print2DArray(grid);
            }

            return FindPossibleAnswers(answer.ToString());
        }

        public override List<string> Decrypt(string message, bool showSteps)
        {
            message = new string(message.Where(c => Char.IsLetter(c)).ToArray()).ToLower();

            var possibleAnswers = new List<string>();
            for (int key = 2; key < Math.Sqrt(message.Length) + 5; key++)
            {
                int size = (int)Math.Ceiling((double)message.Length / key);

                char[,] grid = new char[key, size];

                int buffer = (key * size) - message.Length;
                for (int b = 0; b < buffer; b++) grid[(key - 1) - b, size - 1] = '%';

                var stack = new Stack<char>(message.ToCharArray().Reverse().ToList());
                for (int h = 0; h < key; h++)
                {
                    for (int l = 0; l < size; l++)
                    {
                        if (grid[h, l] != '%') grid[h, l] = stack.Pop();
                    }
                }

                var answer = new StringBuilder();
                for (int l = 0; l < size; l++)
                {
                    for (int h = 0; h < key; h++)
                    {
                        if (grid[h, l] != '%') answer.Append(grid[h, l]);
                    }
                }
                if (showSteps)
                {
                    Console.WriteLine("Key Transposition (Key = " + key + "): " + answer);
                    Print2DArray(grid);
                }
                possibleAnswers.Add(answer.ToString());
            }

            var realWordAnswers = new List<string>();
            foreach (var op in possibleAnswers) realWordAnswers.AddRange(FindPossibleAnswers(op.ToLower()));

            return realWordAnswers;
        }

        public string Encrypt(string message, string keyword, bool showSteps)
        {
            message = new string(message.Where(c => Char.IsLetter(c)).ToArray()).ToLower();
            keyword = new string(keyword.ToLower());

            int size = (int)Math.Ceiling((double)message.Length / keyword.Length);

            char[] keywordTemplate = keyword.ToCharArray();
            char[,] grid = new char[size, keyword.Length];

            int buffer = (keyword.Length * size) - message.Length;
            for (int b = 0; b < buffer; b++) grid[size - 1, (keyword.Length - 1) - b] = '%';

            var stack = new Stack<char>(message.ToCharArray().Reverse().ToList());
            for (int h = 0; h < size; h++)
            {
                for (int l = 0; l < keyword.Length; l++)
                {
                    if (grid[h, l] != '%') grid[h, l] = stack.Pop();
                }
            }

            var answer = new StringBuilder();
            for (int let = 0; let < alphabet.Length; let++)
            {
                int position = keywordTemplate.ToList().IndexOf(alphabet[let]);
                if (position != -1)
                {
                    keywordTemplate[position] = '%';
                    let = -1;

                    for (int h = 0; h < size; h++)
                    {
                        answer.Append(Char.ToUpper(grid[h, position]));
                        if (new Random().Next(5) == 0) answer.Append(" ");
                    }
                }
            }

            if (showSteps)
            {
                Console.WriteLine("Key Transposition (Key = " + keyword + "):");
                Print2DArray(grid);
            }

            return answer.ToString();
        }

        public string Encrypt(string message, int key, bool showSteps)
        {
            message = new string(message.Where(c => Char.IsLetter(c)).ToArray()).ToLower();

            int size = (int)Math.Ceiling((double)message.Length / key);

            char[,] grid = new char[size, key];

            int buffer = (key * size) - message.Length;
            for (int b = 0; b < buffer; b++) grid[size - 1, (key - 1) - b] = '%';

            var stack = new Stack<char>(message.ToCharArray().Reverse().ToList());
            for (int h = 0; h < size; h++)
            {
                for (int l = 0; l < key; l++)
                {
                    if (grid[h, l] != '%') grid[h, l] = stack.Pop();
                }
            }

            var answer = new StringBuilder();
            for (int l = 0; l < key; l++)
            {
                for (int h = 0; h < size; h++)
                {
                    if (grid[h, l] != '%') answer.Append(Char.ToUpper(grid[h, l]));
                }
            }

            if (showSteps)
            {
                Console.WriteLine("Key Transposition (Key = " + key + "):");
                Print2DArray(grid);
            }

            return answer.ToString();
        }
    }
}
