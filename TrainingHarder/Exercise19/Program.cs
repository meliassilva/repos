using System;
using System.Collections.Generic;


namespace Exercise19
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(SumTriple(2, 2));
            Console.WriteLine(SumTriple(12, 10));
            Console.WriteLine(SumTriple(-5, 2));

        }
        public static int SumTriple(int a, int b)
        {
            return a == b ? (a + b) * 3 : a + b;
        }
    }
}
