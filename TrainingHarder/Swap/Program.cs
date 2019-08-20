using System;

namespace Swap
{
    //public class SwapExample
    //{
    //    public static void Main(string[] args)
    //    {

    //        int a = 5;
    //        int b = 10;

    //        Console.WriteLine("Before swap a= " + a + " b= " + b);
    //        a = a * b;  //a=50 (5*10)
    //        b = a / b;  //b=5 (50/10)  
    //        a = a / b;  //a=10 (50/5)

    //        Console.WriteLine("Afeter swap a = " + a + " b = " + b);
    //    }
    //}




    public class SwapExample
    {

        public static void Main(string[] args)
        {
            int number1, number2, temp;
            Console.Write("\nInput the First Number : ");
            number1 = int.Parse(Console.ReadLine());
            Console.Write("\nInput the Second Number : ");
            number2 = int.Parse(Console.ReadLine());
            temp = number1;
            number1 = number2;
            number2 = temp;
            Console.Write("\nAfter Swapping : ");
            Console.Write("\nFirst Number : " + number1);
            Console.Write("\nSecond Number : " + number2);
            Console.Read();
        }

        //public static void Main(string[] args)
        //{

        //    int a = 5, b = 10;
        //    Console.WriteLine("Before swap a= " + a + " b= " + b);
        //    a = a + b; //a=15 (5+10)      
        //    b = a - b; //b=5 (15-10)      
        //    a = a - b; //a=10 (15-5)   
        //    Console.Write("After swap a= " + a + " b= " + b);
        //}
    }
}
