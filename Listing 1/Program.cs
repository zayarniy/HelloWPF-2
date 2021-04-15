// Create a thread of execution. 
//Создание одного потока выполнения
//Шилд 2011
using System;
using System.Threading;

namespace Listing_1
{


    class MyThread
    {
        public int Count;
        string thrdName;

        public MyThread(string name)
        {
            Count = 0;
            thrdName = name;
        }

        // Entry point of thread. 
        // Точка входа в поток
        public void Run()
        {
            Console.WriteLine(thrdName + " starting.");
            do
            {
                Thread.Sleep(1000);
                Console.WriteLine("In " + thrdName +
                                  ", Count is " + Count);
                Count++;
            } while (Count < 20);

            Console.WriteLine(thrdName + " terminating.");
        }
    }

    class MultiThread
    {
        static void Main()
        {
            Console.WriteLine("Main thread starting.");
            // First, construct a MyThread object. 
            // Сначала, создаем объект MyThread
            MyThread mt = new MyThread("Child #1");
            // Next, construct a thread from that object. 
            //Затем, создаем поток из этого объекта
            Thread newThrd = new Thread(mt.Run);
            newThrd.IsBackground = true;//Указываем, что поток фоновый (не приоритетный)
            // Finally, start execution of the thread. 
            newThrd.Start();
            do
            {
                Console.Write(".");
                Thread.Sleep(1000);//спим 100 миллисекунд
                //мы обращаемся к переменной, изменение которой происходит в другом потоке
            } while (mt.Count != 10);

            Console.WriteLine("Main thread ending.");
           // Console.ReadKey();
        }
    }
}

/*
Зачастую в многопоточной программе требуется, чтобы основной поток был последним потоком, 
завершающим ее выполнение. Формально программа продолжает выполняться до тех пор, 
пока не завершатся все ее приоритетные потоки. Поэтому требовать, чтобы основной 
поток завершал выполнение программы, совсем не обязательно. 
Тем не менее этого правила принято придерживаться в многопоточном программировании, 
поскольку оно явно определяет конечную точку программы. В рассмотренной выше программе 
предпринята попытка сделать основной поток завершающим ее выполнение. 
Для этой цели значение переменной Count проверяется в цикле do-while внутри 
метода Main (), и как только это значение оказывается равным 10, 
цикл завершается и происходит поочередный возврат из методов Sleep (). 
Но такой подход далек от совершенства, поэтому далее в будут представлены 
более совершенные способы организации ожидания одного потока до завершения другого.
*/