using System;

namespace Fibonacci
{
    public class FibonacciExample
    {
        public static void Main(string[] args)
        {
            //declare variables
            int n1 = 0, n2 = 1, n3, i, number;

            //ask for the quantity of numbers
            Console.WriteLine("Enter the number of elements: ");

            //parse the number
            number = int.Parse(Console.ReadLine());

            //Print the first two numbers
            Console.WriteLine(n1 + " " + n2 + " ");

            //make the loop to grab the other numbers
            for (i = 2; i < number; ++i)  //starts on 2 first two numbers already set up
            {
                n3 = n1 + n2;
                Console.Write(n3 + " ");
                n1 = n2;
                n2 = n3;
            }

        }
    }
}
