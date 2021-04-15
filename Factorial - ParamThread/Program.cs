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

    class Program
    {


        static ulong S(ulong n)
        {
            ulong s = 0;
            for (ulong i = 1; i <= n; i++) s += i;
            return s;
        }


        static void Main(string[] args)
        {
            Factorial factorial = new Factorial();
            Thread thread = new Thread(new ParameterizedThreadStart(factorial.F));
            thread.Start(5UL);
            thread.Join();
            Console.WriteLine(factorial.Result);
            //Console.WriteLine(S(3));
            Console.ReadKey();
        }
    }
}
