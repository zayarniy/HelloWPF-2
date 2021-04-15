
// Passing an argument to the thread method. 
//В приведенном ниже примере программы демонстрируется передача 
//аргумента потоку.
using System;
using System.Threading;

namespace Listing_6
{

    class Data
    {
        public int Num { get; set; }
        public int Delay { get; set; }
    }

    class MyThread
    {
        public int Count;
        public Thread Thrd;

        // Notice that MyThread is also pass an int value. 
        //Обратите внимание, что MyThread так же использует int value
        public MyThread(string name, Data data)
        {
            Count = 1;

            // Explicitly invoke ParameterizedThreadStart constructor 
            // for the sake of illustration. 
            // Для иллюстрации явно вызываем ParameterizedThreadStart контруктор,
            
            Thrd = new Thread(new ParameterizedThreadStart(this.Run));

            Thrd.Name = name;
            //Data data = new Data() { Delay = 500, Num = 10 };
            // Here, Start() is passed num as an argument. 
            Thrd.Start(data);
        }

        // Notice that this version of Run() has 
        // a parameter of type object. 
        void Run(object data)
        {
            Console.WriteLine(Thrd.Name +
                              " starting with count of " + (data as Data).Num);

            do
            {
                Thread.Sleep((data as Data).Delay);
                Console.WriteLine("In " + Thrd.Name +
                                  ", Count is " + Count);
                Count++;
            } while (Count <= (data as Data).Num);

            Console.WriteLine(Thrd.Name + " terminating.");
        }
    }

    class PassArgDemo
    {
        static void Main()
        {

            // Notice that the iteration count is passed 
            // to these two MyThread objects. 
            MyThread mt = new MyThread("Child #1", new Data() { Delay = 500, Num = 10 });
            MyThread mt2 = new MyThread("Child #2", new Data() { Delay = 1000, Num = 7 });

            do
            {
                Thread.Sleep(100);
            } while (mt.Thrd.IsAlive | mt2.Thrd.IsAlive);

            Console.WriteLine("Main thread ending.");

            Console.Read();

        }
    }
}
/*Аргумент передается потоку в следующей форме метода 
 * Start (). public void Start(object параметр)
Объект, указываемый в качестве аргумента параметр, 
автоматически передается методу, выполняющему роль точки входа в поток. 
Следовательно, для того чтобы передать аргумент потоку, 
достаточно передать его методу Start ().
Для применения параметризированной формы метода Start () 
потребуется следующая форма конструктора класса Thread:
public Thread(ParameterizedThreadStart запуск)
где запуск обозначает метод, вызываемый с целью начать выполнение потока. 
Обратите внимание на то, что в этой форме конструктора запуск имеет 
тип ParameterizedThreadStart, а не Threads start, как в форме, 
использовавшейся в предыдущих примерах. В данном случае 
ParameterizedThreadStart является делегатом, объявляемым следующим образом.
public delegate void ParameterizedThreadStart(object obj)
Как видите, этот делегат принимает аргумент типа object. 
Поэтому для правильного применения данной формы конструктора класса Thread у 
метода, служащего в качестве точки входа в поток, должен быть параметр 
типа object.*/
