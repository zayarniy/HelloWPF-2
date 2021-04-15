using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Натуральное число будем называть хорошим, если оно делится на сумму цифр самого числа

/*
*Написать программу подсчета количества «Хороших» чисел в диапазоне от 1 до 1 000 000 000. Хорошим называется число, которое делится на сумму своих цифр. Реализовать подсчет времени выполнения программы, используя структуру DateTime. 
*/
namespace Good_Numbers
{
    class Task6
    {
        static int SummCyfr(int n)
        {
            int s = 0;
            while (n != 0)
            {
                s = s + n % 10;
                n = n / 10;
            }
            return s;
        }

        static bool GoodNumber(int n)
        {
            return n % SummCyfr(n) == 0;
        }

        static int GoodNumberCounter(int a, int b)
        {
            int k = 0;
            for (int i = a; i <= b; i++)
                if (GoodNumber(i))
                {
                    k++;
                    //if (i % 100000 == 0) Console.WriteLine("i={0} k={1}", i, k);                    
                }
            return k;
        }


        static void Main(string[] args)
        {
            DateTime start=DateTime.Now;
            int k = GoodNumberCounter(1, 1000000000);
            Console.WriteLine(k);
            Console.WriteLine(DateTime.Now - start);
            Console.ReadKey();
        }
    }
}
