using System;
using System.Threading;
/**** Запуск потока на выполнение, сразу после создания ****
Рассмотренная выше программа вполне работоспособна, 
но ее можно сделать более эффективной, внеся ряд простых усовершенствований, 
во-первых, можно сделать так, чтобы выполнение потока начиналось сразу же после 
его создания. Для этого достаточно получить экземпляр объекта типа Thread 
в конструкторе класса MyThread. 
И во-вторых, в классе MyThread совсем не обязательно хранить имя потока, 
поскольку для этой цели в классе Thread специально определено свойство Name.
public string Name { get; set; }
Свойство Name доступно для записи и чтения и поэтому может служить как для запоминания, 
так и для считывания имени потока.
Ниже приведена версия предыдущей программы, в которую внесены упомянутые выше 
усовершенствования.
*/

namespace Listing_2
{
    // An alternate way to start a thread. 

    class MyThread
    {
        public int Count;
        public Thread Thrd;

        public MyThread(string name)
        {
            Count = 0;
            Thrd = new Thread(this.Run);
            Thrd.IsBackground = true;
            Thrd.Name = name; // set the name of the thread 
            Thrd.Start(); // start the thread 
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
            } while (Count < 20);

            Console.WriteLine(Thrd.Name + " terminating.");
        }
    }

    class MultiThreadImproved
    {
        static void Main()
        {
            Console.WriteLine("Main thread starting.");

            // First, construct a MyThread object. 
            MyThread mt = new MyThread("Child #1");

            do
            {
                Console.Write(".");
                Thread.Sleep(100);
            } while (mt.Count != 10);

            Console.WriteLine("Main thread ending.");

            //Console.Read();

        }
    }
}


