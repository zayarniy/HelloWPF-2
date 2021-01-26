using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AsyncDelegate
{
    public delegate int BinaryOp(int x, int y);

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Async Delegate Invocation *****");
             
            // Print out the ID of the executing thread.
            Console.WriteLine("Main() invoked on thread {0}.",
              Thread.CurrentThread.ManagedThreadId);

            BinaryOp b = new BinaryOp(Add);//Создаем делегат
            IAsyncResult ar = b.BeginInvoke(10, 10, null, null);//Вызываем делегат асинхронно
                                                                // This message will keep printing until
                                                                // the Add() method is finished.
                                                                //while (!ar.IsCompleted)
                                                                //{
                                                                //    Console.WriteLine("Doing more work in Main()!");
                                                                //    Thread.Sleep(1000);
                                                                //}

            //AsyncWaitHandle.WaitOne - указываем время ожидания
            //Blocks the current thread until the current WaitHandle receives a signal.
            while (!ar.AsyncWaitHandle.WaitOne(1000))
            /*
                Blocks the current thread until the current WaitHandle receives a signal, 
            using a 32-bit signed integer to specify the time interval and specifying whether to exit 
            the synchronization domain before the wait.           
                 */

            {
                Console.WriteLine("Doing more work in Main()!");
                //Thread.Sleep(1000);
            }


            // Now we know the Add() method is complete.

            int answer = b.EndInvoke(ar);

            Console.WriteLine("10 + 10 is {0}.", answer);
            Console.ReadKey();
            //Правда в этом способе мы постоянно опрашиваем не завершился ли поток.
            //Есть другой более эффективный способ
        }
        

        static int Add(int x, int y)
        {
            // Print out the ID of the executing thread.
            Console.WriteLine("Add() invoked on thread {0}.",
              Thread.CurrentThread.ManagedThreadId);

            // Pause to simulate a lengthy operation.
            Thread.Sleep(10000);
            return x + y;
        }
    }
}

