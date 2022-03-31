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
            var tool = new Skip();
            var results = tool.Decrypt(Console.ReadLine(), true);

            Console.WriteLine();
            Console.WriteLine("Possible Results:");
            foreach (var result in results) Console.WriteLine(result);
            //foreach (var result in FindMostLikely(results, 5)) Console.WriteLine(result);

            /*var tool = new Skip();
            Console.WriteLine(tool.Encrypt(Console.ReadLine(), true));*/
        }

        private static List<string> FindMostLikely(List<string> results, int amount)
        {
            return results.OrderByDescending(r => r.Where(c => Char.IsLetter(c)).Count()).ThenByDescending(r => r, new AnswerComparer()).Take(amount).ToList();
        }
    }
}
