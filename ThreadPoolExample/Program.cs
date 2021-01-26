using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//**** Пока не показывать ****
namespace ThreadPoolExample
{
    public class Printer
    {
        public string Name { get; set; }

        public void PrintNumbers()
        {
            for (int i = 0; i < 10; i++) Console.WriteLine(i);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" * ****Fun with the CLR Thread Pool * ****\n");
            Console.WriteLine("Main thread started. ThreadID = {0}",
            Thread.CurrentThread.ManagedThreadId);
            Printer p = new Printer();
            WaitCallback workItem = new WaitCallback(PrintTheNumbers);
            // nocTaBMTb b oueperib MeTon recflTb pa3. for (int i = 0; i < 10; i++)
            {
                ThreadPool.QueueUserWorkItem(workItem, p);
            }
            Console.WriteLine("All tasks queued");
            Console.ReadLine();
        }

        static void PrintTheNumbers(object state)
        {
            Printer task = (Printer)state; 
            task.PrintNumbers();
        }
    }
}
