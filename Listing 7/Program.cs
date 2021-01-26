// Demonstrate thread priorities. 

using System;
using System.Threading;

namespace Listing_7
{


    class MyThread
    {
        public int Count;
        public Thread Thrd;

        static bool stop = false;
        static string currentName;

        /* Construct a new thread. Notice that this  
           constructor does not actually start the 
           threads running. */
        /*Создаем новый поток. Обратите внимание, что этот конструктор на самом деле
         * не запускает поток на выполнение
         */
        public MyThread(string name)
        {
            Count = 0;
            Thrd = new Thread(this.Run);
            Thrd.Name = name;
            currentName = name;
        }

        // Begin execution of new thread. 
        void Run()
        {
            Console.WriteLine(Thrd.Name + " starting.");
            do
            {
                Count++;
                if (currentName != Thrd.Name)
                {
                    currentName = Thrd.Name;
          //          Console.WriteLine("In " + currentName);
                }

            } while (stop == false && Count < 10000000);
            stop = true;

            Console.WriteLine(Thrd.Name + " terminating.");
        }
    }

    class PriorityDemo
    {
        static void Main()
        {
            MyThread mt1 = new MyThread("High Priority");
            MyThread mt2 = new MyThread("Low Priority");

            // Set the priorities. 
            mt1.Thrd.Priority = ThreadPriority.Highest;
            mt2.Thrd.Priority = ThreadPriority.Lowest;

            // Start the threads. 
            mt1.Thrd.Start();
           // mt1.Thrd.Join(); //- для тех кто не до конца понял Join - можно раскомментировать эту строчку
            mt2.Thrd.Start();

            mt1.Thrd.Join();

            //mt2.Thrd.Join();

            Console.WriteLine();
            Console.WriteLine(mt1.Thrd.Name + " thread counted to " +
                              mt1.Count);
            Console.WriteLine(mt2.Thrd.Name + " thread counted to " +
                              mt2.Count);

            Console.Read();

        }
    }

}
