using System;
using static System.Net.Mime.MediaTypeNames;

namespace Palindrome
{
    public class PalindromeExample
    {
        public static void Main(string[] args)
        {
            int n, r, sum = 0, temp;
            Console.WriteLine("Enter the Number: ");
            n = int.Parse(Console.ReadLine());
            temp = n;
            while (n > 0)
            {
                r = n % 10;
                sum = (sum * 10) + r;
                n = n / 10;
            }
            if (temp == sum)
                Console.WriteLine("Number is a Palindrome");
            else
                Console.WriteLine("Number is not a Palindrome");

      
        }
    }
}
