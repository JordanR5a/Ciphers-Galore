using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ciphers_Galore.Model
{
    public class Bacon : Cipher
    {
        private static Dictionary<string, string[]> binaryToCharacter = new Dictionary<string, string[]>()
        {
            { "00000", new string[] { "A" } },
            { "00001", new string[] { "B" } },
            { "00010", new string[] { "C" } },
            { "00011", new string[] { "D" } },
            { "00100", new string[] { "E" } },
            { "00101", new string[] { "F" } },
            { "00110", new string[] { "G" } },
            { "00111", new string[] { "H" } },
            { "01000", new string[] { "I", "J" } },
            { "01001", new string[] { "K" } },
            { "01010", new string[] { "L" } },
            { "01011", new string[] { "M" } },
            { "01100", new string[] { "N" } },
            { "01101", new string[] { "O" } },
            { "01110", new string[] { "P" } },
            { "01111", new string[] { "Q" } },
            { "10000", new string[] { "R" } },
            { "10001", new string[] { "S" } },
            { "10010", new string[] { "T" } },
            { "10011", new string[] { "U", "V" } },
            { "10100", new string[] { "W" } },
            { "10101", new string[] { "X" } },
            { "10110", new string[] { "Y" } },
            { "10111", new string[] { "Z" } },
        };

        public override List<string> Decrypt(string message, bool showSteps)
        {
            message = new string(message.Where(c => Char.IsLetter(c)).ToArray());

            var letters = SplitInParts(message, 5).ToArray();
            var binary = new StringBuilder[letters.Length];
            

            for (int i = 0; i < letters.Length; i++)
            {
                var letter = letters[i];
                binary[i] = new StringBuilder();
                for (int k = 0; k < letter.Length; k++)
                {
                    if (Char.IsUpper(letter[k])) binary[i].Append("1");
                    else binary[i].Append("0");
                }
            }


            if (showSteps)
            {
                var binaryWork = new StringBuilder("Binary Representation:");
                foreach (var set in binary) binaryWork.Append(" ").Append(set);
                Console.WriteLine(binaryWork.ToString());

                var plainWork = new StringBuilder("Plain Text: ");
                foreach (var set in binary)
                {
                    var conversions = binaryToCharacter[set.ToString()];
                    if (conversions.Length == 1) plainWork.Append(conversions[0].ToLower());
                    else
                    {
                        plainWork.Append("(");
                        foreach (var op in binaryToCharacter[set.ToString()])
                            plainWork.Append(op.ToLower()).Append("/");
                        plainWork.Length--;
                        plainWork.Append(")");
                    }
                }
                Console.WriteLine(plainWork.ToString());
                    
            }

            var binaries = binary.Select(set => set.ToString()).ToArray();

            var optionalConversions = new List<string>() { "" };
            for (int i = 0; i < binary.Length; i++)
            {
                var conversions = binaryToCharacter[binaries[i]];
                switch (conversions.Length)
                {
                    case 1:
                        optionalConversions = optionalConversions.Select(path => path + conversions[0]).ToList();
                        break;
                    case 2:
                        var newPaths = optionalConversions.Select(path => path).ToList();
                        newPaths = newPaths.Select(path => path + conversions[1]).ToList();
                        optionalConversions = optionalConversions.Select(path => path + conversions[0]).ToList();
                        optionalConversions.AddRange(newPaths);
                        break;
                }
            }

            var realWordAnswers = new List<string>();
            foreach (var op in optionalConversions) realWordAnswers.AddRange(FindPossibleAnswers(op.ToLower()));

            return realWordAnswers;
            
        }

        //https://stackoverflow.com/questions/4133377/splitting-a-string-number-every-nth-character-number
        public static IEnumerable<String> SplitInParts(String s, Int32 partLength)
        {
            if (s == null)
                throw new ArgumentNullException(nameof(s));
            if (partLength <= 0)
                throw new ArgumentException("Part length has to be positive.", nameof(partLength));

            for (var i = 0; i < s.Length; i += partLength)
                yield return s.Substring(i, Math.Min(partLength, s.Length - i));
        }

        public override string Encrypt(string message, bool showSteps)
        {
            message = new string(message.Where(c => Char.IsLetter(c)).ToArray());

            var binary = new StringBuilder();

            foreach (var c in message)
                foreach (var set in binaryToCharacter)
                    if (set.Value.ToList().Contains(c.ToString().ToUpper()))
                        binary.Append(set.Key);

            var temp = binary.Length;
            var result = new StringBuilder();

            foreach (var c in binary.ToString())
            {
                if (int.Parse(c.ToString()) == 0) result.Append(Library.Letter.ToString().ToLower());
                else if (int.Parse(c.ToString()) == 1) result.Append(Library.Letter.ToString().ToUpper());
                if (new Random().Next(2) == 1) result.Append(" ");
            }

            return result.ToString();
        }
    }
}
