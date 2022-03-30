using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ciphers_Galore.Model
{
    public class Reverse : Cipher
    {

        public override List<string> Decrypt(string message, bool showSteps)
        {
            return FindPossibleAnswers(new string(message.ToCharArray().Reverse().Where(c => Char.IsLetter(c)).ToArray()).ToLower());
        }

        public override string Encrypt(string message, bool showSteps)
        {
            return null;
        }
    }
}
