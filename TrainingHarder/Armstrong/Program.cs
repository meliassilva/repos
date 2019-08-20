using System;
using static System.Net.Mime.MediaTypeNames;

namespace Armstrong
{
    public class ArmstrongExample
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
                    int n;
                    int r;
                    int sum = 0;
                    int temp;


                    Console.WriteLine("Enter the Number: ");
                    n = int.Parse(Console.ReadLine());
                    temp = n;
                    while (n > 0)
                    {
                        r = n % 10;
                        sum = sum + (r * r * r);
                        n = n / 10;
                    }
                    if (temp == sum)
                        Console.WriteLine("Armstrong Number");
                    else
                        Console.WriteLine("Not Armstrong Number");
                }
                else
                {
                    done = true;
                }
            }
        }

    }
}
