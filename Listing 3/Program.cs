// Create multiple threads of execution. 

//Можно создавать множество потоков одновременно, запуская их сразу после создания
using System;
using System.Threading;
namespace Listing_3
{
    //класс потоком выполнения
    class MyThread
    {
        public int Count;
        public Thread Thrd;

        //Передаем в конструктор имя потока
        public MyThread(string name)
        {
            Count = 0;
            //помещаем в поток метод - точку входа 
            Thrd = new Thread(this.Run);
            Thrd.Name = name;
            Thrd.Start();//стартуем поток
        }

        // Entry point of thread. 
        void Run()
        {
            Console.WriteLine(Thrd.Name + " starting.");

            do
            {
                Thread.Sleep(1000);
                Console.WriteLine("In " + Thrd.Name +
                                  ", Count is " + Count);
                Count++;
            } while (Count < 10);

            Console.WriteLine(Thrd.Name + " terminating.");
        }
    }

    class MoreThreads
    {
        static void Main()
        {
            Console.WriteLine("Main thread starting at "+DateTime.Now.ToLongTimeString());

            // Construct three threads. 
            //создаем три потока и сразу запускаем их
            MyThread mt1 = new MyThread("Child #1");
            MyThread mt2 = new MyThread("Child #2");
            MyThread mt3 = new MyThread("Child #3");

            do
            {
                Console.Write(".");
                Thread.Sleep(1000);//Время через которое закончится основной поток
            } while (mt1.Count < 10 ||
                     mt2.Count < 10 ||
                     mt3.Count < 10);

            //Основной поток может закончиться позже, но не раньше пока не закончатся приоритетные потоки (нужно поменять время в Thread.Sleep(150000) на меньше
            Console.WriteLine("Main thread ending at "+DateTime.Now.ToLongTimeString());

            //Console.Read();

        }
    }
}
