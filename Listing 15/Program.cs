// Use a manual event object.   
/*
 Представляет событие синхронизации потока, которое при получении сигнала необходимо сбросить вручную. 
 Этот класс не наследуется. 
*/
using System;
using System.Threading;

// This thread signals the event passed to its constructor.   
class MyThread
{
    public Thread Thrd;
    ManualResetEvent mre;

    public MyThread(string name, ManualResetEvent evt)
    {
        Thrd = new Thread(this.Run);
        Thrd.Name = name;
        mre = evt;
        Console.WriteLine(name+" is started");
        Thrd.Start();
    }

    // Entry point of thread.    
    void Run()
    {
        Console.WriteLine("Выполнение работы потока " + Thrd.Name);

        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine(Thrd.Name);
            Thread.Sleep(500);
        }

        Console.WriteLine(Thrd.Name + " Выполнили!");

        // Signal the event.
        
        mre.Set();//освобождает все три потока
    }
}

class ManualEventDemo
{
    public static void Main()
    {
        //создаем событие для потока Main и указываем, что о событии не нужно уведомлять
        ManualResetEvent evtObj = new ManualResetEvent(false);

        MyThread mt1 = new MyThread("Event Thread 1", evtObj);

        Console.WriteLine("Главный поток запускается и ожидает уведомления о установке событие (Set).");
        // Wait for signaled event. 
        //Ожидаем сигнала о завершении выполнения потока
        evtObj.WaitOne();//Comment it for present

        Console.WriteLine("Main thread received first event.");
        //Как только получили уведомление

        // Reset the event.
        //Сбосываем событие
        evtObj.Reset();

        mt1 = new MyThread("Event Thread 2", evtObj);

        // Wait for signaled event. 
        //Ожидаем сигнала о завершении выполнения потока
        evtObj.WaitOne();//Comment it for present

        //Сбосываем событие
        evtObj.Reset();

        mt1 = new MyThread("Event Thread 3", evtObj);
        // Wait for signaled event. 
        //Ожидаем сигнала о завершении выполнения потока
        evtObj.WaitOne();//Comment it for present


        mt1 = new MyThread("Event Thread 4", evtObj);

        // Wait for signaled event. 
        //Ожидаем сигнала о завершении выполнения потока
        evtObj.WaitOne();//Comment it for present


        Console.WriteLine("Main thread received second event.");
        Console.ReadKey();
    }
}

/*
Применяются события очень просто. Так, для события типа ManualResetEvent порядок применения следующий. Поток, ожидающий некоторое событие, вызывает метод WaitOne() для событийного объекта, представляющего данное событие. Если событийный объект находится в сигнальном состоянии, то происходит немедленный возврат из метода WaitOne(). В противном случае выполнение вызывающего потока приостанавливается до тех пор, пока не будет получено уведомление о событии. Как только событие произойдет в другом потоке, этот поток установит событийный объект в сигнальное состояние, вызвав метод Set(). Поэтому метод Set() следует рассматривать как уведомляющий о том, что событие произошло.

После установки событийного объекта в сигнальное состояние произойдет немедленный возврат из метода WaitOne(), и первый поток возобновит свое выполнение. А в результате вызова метода Reset() событийный объект возвращается в несигнальное состояние.  

*/