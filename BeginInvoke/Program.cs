using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeginInvoke
{
    class Program
    {
        static long Factorial(int x)
        {
            long res = 1;
            while (x > 1)
            {
                res *= x;
                x--;
            }
            return res;
        }

        static void Main(string[] args)
        {

            //Этот пример работает в .NET, но не работает Core.NET
            Func<int, long> get_factorial = Factorial;
            get_factorial.BeginInvoke(10, result =>
            {
                get_factorial.EndInvoke(result);
                Console.WriteLine(result);
            }, null);            
        }
    }
}
