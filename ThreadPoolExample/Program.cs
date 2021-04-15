using System;
using System.Threading;

//**** Пока не показывать ****
namespace ThreadPoolExample
{
    public class Printer
    {
        public string Name { get; set; }
        object obj = new object();

        public void PrintNumbers()
        {
            lock (obj)
            {
                for (int i = 0; i < 10; i++) Console.Write(i);
                Console.WriteLine();
            }
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
            ThreadPool.QueueUserWorkItem(workItem, p);
            ThreadPool.QueueUserWorkItem(workItem, p);
            ThreadPool.QueueUserWorkItem(workItem, p);
            ThreadPool.QueueUserWorkItem(workItem, p);
            ThreadPool.QueueUserWorkItem(workItem, p);
            Console.WriteLine("All tasks queued");
            //Thread.Sleep(10000);
            Console.WriteLine("Main thread ending");
            Console.ReadLine();
        }

        static void PrintTheNumbers(object state)
        {
            Printer task = (Printer)state; 
            task.PrintNumbers();
        }
    }
}
