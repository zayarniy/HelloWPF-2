using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

//Пример использования CallerMemberName

namespace ConsoleTest
{
    class Program
    {

        static void Write([CallerMemberName] string memberName = "")
        {
            Console.WriteLine("Called by:"+memberName);
        }

        static void Method()
        {
            Write();
        }
        static void Main(string[] args)
        {
            Write();//Main
            Method();//Method
            Console.ReadKey();
        }
        
    }
}
