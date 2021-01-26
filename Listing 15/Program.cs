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
        
        mre.Set();
    }
}

class ManualEventDemo
{
    public static void Main()
    {
        ManualResetEvent evtObj = new ManualResetEvent(false);

        MyThread mt1 = new MyThread("Event Thread 1", evtObj);

        Console.WriteLine("Main thread waiting for event.");

        // Wait for signaled event. 
        evtObj.WaitOne();//Comment it for present

        Console.WriteLine("Main thread received first event.");


        // Reset the event. 
        evtObj.Reset();


        mt1 = new MyThread("Event Thread 2", evtObj);

        // Wait for signaled event. 
        evtObj.WaitOne();//Comment it for present


        mt1 = new MyThread("Event Thread 3", evtObj);

        // Wait for signaled event. 
        evtObj.WaitOne();//Comment it for present

        Console.WriteLine("Main thread received second event.");
        Console.ReadKey();
    }
}