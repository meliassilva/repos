using System;

namespace MakeDifference
{
    class Program
    {
        static void Main(string[] args)
        {
            // declare variables
            int num1 = 0; int num2 = 0;

            //Display title 
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            //Ask th user
            Console.WriteLine("Poe um numero ai e aperte enter");
            num1 = Convert.ToInt32(Console.ReadLine());

            //Second Number

            Console.WriteLine("Poe mais um numero ai cara e aperte enter de novo");
            num2 = Convert.ToInt32(Console.ReadLine());

            //Choose option

            Console.WriteLine("Choose an option ai mane:");
            Console.WriteLine("\ta - Continha de mais");
            Console.WriteLine("\ts - Continha de menas");
            Console.WriteLine("\tm - Multiplica a bagaca ai");
            Console.WriteLine("\td - Divedi ai ");
            Console.Write("Your option? ");

            // Switch to chancge
            switch (Console.ReadLine())
            {
                case "a":
                    Console.WriteLine($"Your result: {num1} + {num2} = " + (num1 + num2));
                    break;
                case "s":
                    Console.WriteLine($"Your result: {num1} - {num2} = " + (num1 - num2));
                    break;
                case "m":
                    Console.WriteLine($"Your result: {num1} * {num2} = " + (num1 * num2));
                    break;
                case "d":
                    Console.WriteLine($"Your result: {num1} / {num2} = " + (num1 / num2));
                    break;
            }

            //Wait for the user to respond before closing
            Console.WriteLine("Pressiona entra de novo ai se quiser fechar a bagaca");
            Console.ReadKey();

        }
    }
}
