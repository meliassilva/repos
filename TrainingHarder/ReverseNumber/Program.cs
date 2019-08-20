using System;

namespace ReverseNumber
{
    class Program
    {
        public static void Main(string[] args)
        {
            bool done = false;

            while (!done)
            {
                Console.WriteLine("Press q to restart: ");

                String restart = Console.ReadLine();

                if (restart == "q")
                {
                    int n, reverse = 0, rem;
                    Console.WriteLine("Enter a number: ");
                    n = int.Parse(Console.ReadLine());
                    while (n != 0)
                    {
                        rem = n % 10;
                        reverse = reverse * 10 + rem;
                        n /= 10;
                    }
                    Console.Write("Reversed Number: " + reverse);
                }
                else
                {
                    done = true;
                }
            }
        }
    }
}
