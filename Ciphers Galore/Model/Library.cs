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
    }
}
