using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ciphers_Galore.Model
{
    public class Library
    {
        private string[] words;

        public int LargestWordLength 
        { 
            get
            {
                return words.OrderByDescending(s => s.Length).First().Length;
            } 
        }

        public string RealWord
        {
            get
            {
                return words[new Random().Next(words.Length - 1)];
            }
        }

        public char Letter
        {
            get
            {
                var word = RealWord;
                return word[new Random().Next(word.Length - 1)];
            }
        }

        public int Size { get { return words.Length; } }

        public Library()
        {
            words = File.ReadAllLines(@"..\..\..\Full.txt");
        }

        public bool IsRealWord(string possibleWord)
        {
            if (words.Contains(possibleWord.ToLower())) return true;
            else return false;
        }

        public string GetRealWord(int length)
        {
            return GetRealWord(length, new Dictionary<char, int>());
        }

        public string GetRealWord(int length, Dictionary<char, int> requiredLetterPositions)
        {
            if (requiredLetterPositions.Any(set => set.Value >= length)) return null;

            int attempts = 0;
            while (true)
            {
                string word = RealWord;
                if (word.Length == length)
                {
                    bool accept = true;
                    foreach (var letPos in requiredLetterPositions)
                        if (!word.Substring(letPos.Value, 1).Equals(letPos.Key.ToString().ToLower()))
                            accept = false;

                    if (accept) return word;
                }
                if (attempts++ > 250) return null;
            }
        }
    }
}
