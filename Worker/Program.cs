using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerExample
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
            BackgroundWorker worker = new BackgroundWorker();            
            worker.DoWork += (s, e) =>
              {
                  BackgroundWorker w = (BackgroundWorker)s;
                  e.Result = Factorial((int)e.Argument);
                  if (worker.CancellationPending==true)
                  {
                      e.Cancel = true;
                      return;
                  }
              };
            worker.ProgressChanged += (_, e) => Console.WriteLine("{0}", e);
            worker.RunWorkerCompleted += (_, e) => Console.WriteLine("Result:{0}",e.Result);
            worker.RunWorkerAsync(5);
            Console.ReadKey();
        }
    }
}
