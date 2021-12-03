using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
/*
Написать приложение, считающее в раздельных потоках: 
1) факториал числа N, которое вводится с клавиатуры; 
2) сумму целых чисел до N.

*/
namespace Homework5
{

class Factorial
    {
        public ulong Result { get; private set; }
        public void F(object n)
        {
            ulong f = 1;
            ulong n1 = (ulong)((ulong)n);
            for (ulong i = 2; i <= n1; i++) f = f * i;
            this.Result = f;
        }
    }

    class Sum
    {
        public static ulong Result { get; set; } = 0;
        public static void S(object n)
        {
            Result = 0;
            for (ulong i = 1; i <= ((ulong)n); i++) Result += i;            
        }

    }

    class Program
    {



        static void Main(string[] args)
        {
            Factorial factorial = new Factorial();
            Thread thread = new Thread(new ParameterizedThreadStart(factorial.F));
            Thread thread2 = new Thread(new ParameterizedThreadStart(Sum.S));
            thread.Start(5UL);
            thread2.Start(10UL);
            thread.Join();
            thread2.Join();
            Console.WriteLine($"F={factorial.Result}");
            Console.WriteLine($"Sum={Sum.Result}");            
            Console.ReadKey();
        }
    }
}
