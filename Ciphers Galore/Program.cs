using System;
using System.Collections.Generic;
using System.Linq;
using Ciphers_Galore.Model;

namespace Ciphers_Galore
{
    public class Program
    {
        static void Main(string[] args)
        {
            var tool = new Caesar();
            var results = tool.Decrypt(Console.ReadLine(), "If you are wanting to know the best TV show of all time, stay tuned", true);

            Console.WriteLine();
            Console.WriteLine("Possible Answers:");
            //foreach (var result in results) Console.WriteLine(result);
            foreach (var result in FindMostLikely(results, 25)) Console.WriteLine(result);

            /*var tool = new Vigenere();
            Console.WriteLine(tool.Encrypt(Console.ReadLine(), "NERDY", true));*/


        }

        private static List<string> FindMostLikely(List<string> results, int amount)
        {
            return results.OrderByDescending(r => r, new AnswerComparer()).Take(amount).ToList();
        }
    }
}
