using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ciphers_Galore.Model
{
    public class Vigenere : Cipher
    {
        private string GenerateKey(string message, string key)
        {
            key = new string(key.Where(c => Char.IsLetter(c)).ToArray());
            int x = message.Length;

            for (int i = 0; ; i++)
            {
                if (x == i)
                    i = 0;
                if (key.Length == message.Length)
                    break;
                key += (key[i]);
            }
            return key;
        }

        public List<string> Decrypt(string message, string keyword, bool showSteps)
        {
            message = new string(message.Where(c => Char.IsLetter(c)).ToArray());
            keyword = GenerateKey(message, keyword);

            var answer = new StringBuilder();
            if (showSteps) Console.WriteLine("Processing...");
            for (int i = 0; i < message.Length; i++)
            {
                int value = (message[i] - keyword[i] + 26) % 26;
                value += 'a';
                answer.Append((char)value);
                if (showSteps) Console.WriteLine($"Row {keyword[i]} at value {message[i]} is column {(char)value}");
            }

            if (showSteps) Console.WriteLine("Raw: " + answer.ToString());

            return FindPossibleRealWordAnswers(answer.ToString());
        }

        public string Encrypt(string message, string keyword, bool showSteps)
        {
            message = new string(message.Where(c => Char.IsLetter(c)).ToArray()).ToUpper();
            keyword = GenerateKey(message, keyword);

            var answer = new StringBuilder();
            for (int i = 0; i < message.Length; i++)
            {
                int value = (message[i] + keyword[i]) % 26;
                value += 'A';
                answer.Append((char)value);
            }
            return answer.ToString();
        }
    }
}
