using System;

namespace Exercise15
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(remove_char("w3resource", 1));
            Console.WriteLine(remove_char("w3resource", 9));
            Console.WriteLine(remove_char("w3resource", 0));
        }
        public static string remove_char(string str, int n)
        {
            return str.Remove(n, 1);
        }
    }
}
