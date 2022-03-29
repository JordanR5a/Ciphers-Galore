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
            var tool = new Bacon();
            var results = tool.Decrypt(Console.ReadLine(), true);

            Console.WriteLine();
            Console.WriteLine("Possible Results:");
            //foreach (var result in results) Console.WriteLine(result);
            foreach (var result in FindMostLikely(results, 5)) Console.WriteLine(result);
        }

        private static List<string> FindMostLikely(List<string> results, int amount)
        {
            return results.OrderByDescending(r => r, new AnswerComparer()).Take(amount).ToList();
        }
    }
}
