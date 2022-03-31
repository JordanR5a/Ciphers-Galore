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
            message = new string(message.ToCharArray().Reverse().Where(c => Char.IsLetter(c)).ToArray()).ToUpper();
            
            var result = new StringBuilder();
            foreach (var c in message)
            {
                if (new Random().Next(1) == 0) result.Append(c.ToString().ToLower());
                else result.Append(c.ToString().ToUpper());
                if (new Random().Next(2) == 1) result.Append(" ");
            }

            return result.ToString();
        }
    }
}
