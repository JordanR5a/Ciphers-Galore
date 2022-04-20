﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ciphers_Galore.Model
{
    public class Frequency : Cipher
    {
        private static readonly char[] LetterFrequency = new char[] { 'e', 't', 'a', 'o', 'i', 'n', 'h', 's', 'r', 'd', 'l', 'c', 'm', 'u', 'w', 'f', 'g', 'y', 'p', 'b', 'v', 'k', 'j', 'x', 'q', 'z' };

        public void GetFrequencies(string message, bool showSteps)
        {
            message = new string(message.Where(c => Char.IsLetter(c)).ToArray()).ToLower();

            var cipherFrequency = new Dictionary<char, int>();
            foreach (char c in message)
                if (cipherFrequency.ContainsKey(c)) cipherFrequency[c] += 1;
                else cipherFrequency.Add(c, 1);
            cipherFrequency = cipherFrequency.OrderByDescending(c => c.Value) as Dictionary<char, int>;

            if (showSteps)
            {
                Console.WriteLine("Frequency Chart:");
                foreach (var c in cipherFrequency) Console.WriteLine($"{c.Key}: {c.Value}");
                Console.WriteLine();
            }

        }

    }
}
