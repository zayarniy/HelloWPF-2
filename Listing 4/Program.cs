﻿// Create multiple threads of execution. 

//Можно создавать множество потоков одновременно
/*
 * При выполнении этой версии программы результат получается таким же, как и прежде. 
 * Единственное отличие заключается в том, что в ней используется свойство Is Alive 
 * для отслеживания момента окончания порожденных потоков.
 */
using System;
using System.Threading;
namespace Listing_4
{


    class MyThread
    {
        public int Count;
        public Thread Thrd;

        public MyThread(string name)
        {
            Count = 0;
            Thrd = new Thread(this.Run);
            Thrd.Name = name;
            Thrd.Start();
        }

        // Entry point of thread. 
        void Run()
        {
            Console.WriteLine(Thrd.Name + " starting.");

            do
            {
                Thread.Sleep(2000);
                Console.WriteLine("In " + Thrd.Name +
                                  ", Count is " + Count);
                Count++;
            } while (Count < 10);

            Console.WriteLine(Thrd.Name + " terminating.");
        }
    }

    // Use IsAlive to wait for threads to end. 
    class MoreThreads
    {
        static void Main()
        {
            Console.WriteLine("Main thread starting.");

            // Construct three threads. 
            MyThread mt1 = new MyThread("Child #1");
            MyThread mt2 = new MyThread("Child #2");
            MyThread mt3 = new MyThread("Child #3");

            do
            {
                Console.Write(".");
                Thread.Sleep(500);
            } while (mt1.Thrd.IsAlive &&
                     mt2.Thrd.IsAlive &&
                     mt3.Thrd.IsAlive);

            Console.WriteLine("Main thread ending.");

            Console.Read();

        }
    }
}
