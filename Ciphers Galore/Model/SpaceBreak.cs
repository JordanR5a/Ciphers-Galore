using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ciphers_Galore.Model
{
    public class SpaceBreak : Cipher
    {
        public List<string> Decrypt(string message, bool showSteps)
        {
            message = new string(message.Where(c => Char.IsLetter(c)).ToArray()).ToLower();
            if (showSteps) Console.WriteLine("Plain Text: " + message);

            return FindPossibleAnswers(message);
        }
    }
}
