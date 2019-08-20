using System;

namespace _11
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int age;
            Console.Write("Enter your age ");
            age = Convert.ToInt32(Console.ReadLine());
            Console.Write("You look younger that {0} ", age);
        }
    }
}
