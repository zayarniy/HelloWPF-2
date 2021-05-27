// Stopping a thread by use of Abort(). 
/*Abort - Вызывает исключение ThreadAbortException в вызвавшем его потоке для того, 
 * чтобы начать процесс завершения потока. 
 * Вызов данного метода обычно завершает поток.*/
using System;
using System.Threading;

class MyThread
{
    public Thread Thrd;

    public MyThread(string name)
    {
        Thrd = new Thread(this.Run);
        Thrd.Name = name;
        Thrd.Start();
    }

    // This is the entry point for thread.  
    void Run()
    {
        try
        {
            Console.WriteLine(Thrd.Name + " starting.");
            //Делаем полезную работу
            for (int i = 1; i <= 10000; i++)
            {
                Console.Write(i + " ");
                if ((i % 10) == 0)
                {
                    Console.WriteLine();
                    Thread.Sleep(250);
                }
            }
            Console.WriteLine(Thrd.Name + " exiting.");
        }
        catch(ThreadAbortException)
        {
            Console.WriteLine("Exception!");
        }
    }
}

class StopDemo
{
    static int seconds = 3;
    static void Main()
    {
        MyThread mt1 = new MyThread("My Thread");

        Thread.Sleep(seconds*1000); // позволим выполняться потоку seconds секунд

        Console.WriteLine("Stopping thread.");
        mt1.Thrd.Abort();

        mt1.Thrd.Join(); // wait for thread to terminate 

        Console.WriteLine("Main thread terminating.");
        Console.ReadKey();
    }
}