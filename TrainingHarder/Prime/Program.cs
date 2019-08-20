using System;

namespace Prime
{
    public class PrimeNumber
    {
        static void Main(string[] args)
        {

            int n, i, m = 0, flag = 0;
            Console.WriteLine("Enter the Number to check Prime: ");
            n = int.Parse(Console.ReadLine());
            m = n / 2;
            for (i = 2; i <= m; i++)
            {
                if (n % i == 0)
                {
                    Console.WriteLine("Number is not Prime");
                    flag = 1;
                    break;
                }
            }
            if (flag == 0)
                Console.WriteLine("Number is Prime");
        }
    }
}
