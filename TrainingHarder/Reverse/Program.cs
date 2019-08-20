using System;

namespace Sum
{
    public class Sum
    {
        public static void Main(string[] args)
        {
            bool done = false;

            while (!done)
            {
                Console.WriteLine(" press q to restarrt program");

                String restart = Console.ReadLine();

                if (restart == "q")
                {
                    int n, sum = 0, m;
                    Console.WriteLine("Enter a number");
                    n = int.Parse(Console.ReadLine());
                    while (n > 0)
                    {
                        m = n % 10;
                        sum = sum + m;
                        n = n / 10;
                    }

                    Console.WriteLine("Sum is = " + sum);

                }
                else
                {
                    done = true;
                }
            }
        }
    }
}
