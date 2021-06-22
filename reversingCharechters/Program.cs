using System;

namespace reversingCharechters
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Input=> ");
            string input = Console.ReadLine();
            string output = "";

            for (int i = input.Length - 1; i >= 0; i--)
            {
                output += input[i];
            }

            Console.WriteLine($"Output=> {output}");
        }
    }
}