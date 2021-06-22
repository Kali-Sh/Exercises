using System;

namespace removingRepeatingCharechters
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            int counter = 0;

            for(int i=0; i<input.Length-1; i++)
            {
                counter = 0;
                for (int j=i+1; j<input.Length-1; j++)
                {
                    if (input[i] == input[j])
                    {
                        counter++;
                    }
                    else
                    {
                        break;
                    }
                }
                if (counter != 0)
                {
                    input = input.Remove(i, counter);
                }
                
            }
            Console.WriteLine(input);
        }
    }
}
