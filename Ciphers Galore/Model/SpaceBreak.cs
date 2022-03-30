using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ciphers_Galore.Model
{
    public class SpaceBreak : Cipher
    {
        public override List<string> Decrypt(string message, bool showSteps)
        {
            message = new string(message.Where(c => Char.IsLetter(c)).ToArray()).ToLower();
            if (showSteps) Console.WriteLine("Plain Text: " + message);

            return FindPossibleAnswers(message);
        }

        public override string Encrypt(string message, bool showSteps)
        {
            message = new string(message.Where(c => !Char.IsWhiteSpace(c)).ToArray()).ToUpper();
            if (showSteps) Console.WriteLine("Combined Text: " + message);

            var encrypted = new StringBuilder();
            for (int i = 0; i < message.Length; i++)
            {
                if (i % 2 == 0 && i != 0) encrypted.Append(" ");
                encrypted.Append(message[i]);
            }
            if (encrypted[encrypted.Length - 2].Equals(' ')) encrypted.Append("X");

            return encrypted.ToString();
        }
    }
}
