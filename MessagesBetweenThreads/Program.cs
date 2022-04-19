// Create multiple threads of execution. 

//Можно создавать множество потоков одновременно
/*
 * При выполнении этой версии программы результат получается таким же, как и прежде. 
 * Единственное отличие заключается в том, что в ней используется свойство Is Alive 
 * для отслеживания момента окончания порожденных потоков.
 */
using System;
using System.Threading;
namespace MessagesBetweenThreads
{

    class MyThread
    {
        public int Count;
        public Thread Thrd;
        bool StartStop = false;
        ManualResetEvent mre;

        public MyThread(string name,ManualResetEvent evt)
        {
            Count = 0;
            Thrd = new Thread(new ThreadStart(this.Run));
            Thrd.Name = name;            
            Thrd.Start();
            mre = evt;
        }
        

        // Entry point of thread. 
        void Run()
        {
            Console.WriteLine(Thrd.Name + " starting.");

            do
            {
                mre.WaitOne();
                Thread.Sleep(2000);
                Console.WriteLine("In " + Thrd.Name +
                                  ", Count is " + Count);
                Count++;
            } while (Count < 100);

            Console.WriteLine(Thrd.Name + " terminating.");
        }
    }

    // Use IsAlive to wait for threads to end. 
    class MoreThreads
    {
        static bool StartStop = false;
        static void Main()
        {
            ManualResetEvent evtObj=new ManualResetEvent(false);
            Console.WriteLine("Main thread starting.");

            // Construct three threads. 
            MyThread mt1 = new MyThread("Child #1",evtObj);
            MyThread mt2 = new MyThread("Child #2", evtObj);
            MyThread mt3 = new MyThread("Child #3", evtObj);

            //mt1.Thrd.Join();
            //Console.WriteLine("Child #1 joined.");

            //mt2.Thrd.Join();
            //Console.WriteLine("Child #2 joined.");
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    if (key.KeyChar == '1')
                    {
                        //Start/Stop 1
                        StartStop = !StartStop;
                        if (StartStop) evtObj.Set(); else evtObj.Reset();
                    }
                    if (key.KeyChar == '2')
                    {
                        //Start/Stop 2
                    }
                    if (key.KeyChar == '3')
                    {
                        //Start/Stop 3
                    }
                }
                Thread.Sleep(100);
            }
            mt3.Thrd.Join();
            Console.WriteLine("Child #3 joined.");
            Console.WriteLine("Main thread ending.");



        }
    }
}
