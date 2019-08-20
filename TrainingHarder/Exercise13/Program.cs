using System;

namespace Exercise13
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int num;
            Console.WriteLine("Enter a number: ");
            num = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("{0}{0}{0}", num);
            Console.WriteLine("{0} {0}", num);
            Console.WriteLine("{0} {0}", num);
            Console.WriteLine("{0} {0}", num);
            Console.WriteLine("{0}{0}{0}", num);


        }
    }
}
