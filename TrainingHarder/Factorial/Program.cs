using System;

namespace Factorial
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int i;
            int fact = 1;
            int number;

            Console.WriteLine("Enter the number: ");
            number = int.Parse(Console.ReadLine());
            for (i = 1; i <= number; i++)
            {
                fact = fact * i;
            }
            Console.WriteLine("Factorial of " + number + "is: " + fact);
        }
    }
}
