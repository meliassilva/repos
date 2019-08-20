using System;

namespace DecimaltoBinary
{
    public class ConversionExample
    {
        public static void Main(string[] args)
        {
            bool done = false;

            while (!done)
            {
                Console.WriteLine(" press q to restart program");

                String restart = Console.ReadLine();

                if (restart == "q")
                {
                    int n, i;
                    int[] a = new int[10];
                    Console.WriteLine("Enter the number to convert");
                    n = int.Parse(Console.ReadLine());
                    for (i = 0; n > 0; i++)
                    {
                        a[i] = n % 2;
                        n = n / 2;
                    }
                    Console.WriteLine("Binary of the given number = ");
                    for (i = i - 1; i >= 0; i--)
                    {
                        Console.WriteLine(a[i]);
                    }
                }

                else
                {
                    done = true;
                }


            }

        }
    }
}
