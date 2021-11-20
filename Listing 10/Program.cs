// Use Wait() and Pulse() to create a ticking clock. 

using System;
using System.Threading;




class TickTock
{
    object lockOn = new object();


    #region Работающая версия TickTock
    public void Tick(bool running)
    {
        lock (lockOn)
        {
            //Если поток не нужно запускать
            if (!running)
            { // останавливаем поток (return ниже)
                Monitor.Pulse(lockOn); // уведомляем все ждущие потоки
                return;
            }
            else
            {
                Console.Write("Tick ");
                Monitor.Pulse(lockOn); // позволяем потоку Tock выполнить

                Monitor.Wait(lockOn); // приостанавливаемся, пока не освободится lockOn (wait for Tock() to complete )
            }
        }
    }

    public void Tock(bool running)
    {
        lock (lockOn)
        {
            if (!running)
            { // stop the clock 
                Monitor.Pulse(lockOn); // notify any waiting threads 
                return;
            }
            else
            {
                Console.WriteLine("Tock");
                Monitor.Pulse(lockOn); // let Tick() run 

                Monitor.Wait(lockOn); // wait for Tick() to complete 
            }
        }
    }
    #endregion

    #region Не работающая версия TickTock (Listing 11) (состояние гонки и взаимоблокировки)
    //Просто запустить, чтобы показать состояние гонки
    //public void Tick(bool running)
    //{


    //    Console.Write("Tick ");

    //}

    //public void Tock(bool running)
    //{


    //    Console.WriteLine("Tock");

    //}
    #endregion
}

class MyThread
{
    public Thread Thrd;
    TickTock ttOb;

    // Construct a new thread. 
    public MyThread(string name, TickTock tt)
    {
        Thrd = new Thread(this.Run);
        //Thrd.IsBackground = true;
        ttOb = tt;
        Thrd.Name = name;
        Thrd.Start();
    }

    // Begin execution of new thread. 
    void Run()
    {
        if (Thrd.Name == "Tick")
        {
            //Выполняем 10 раз метод Tick
            for (int i = 0; i < 10; i++) ttOb.Tick(true);
            ttOb.Tick(false);
        }
        else
        {
            //Выполняем 10 раз метод Tock
            for (int i = 0; i < 10; i++) ttOb.Tock(true);
            ttOb.Tock(false);
        }
    }
}

class TickingClock
{
    static void Main()
    {
        TickTock tt = new TickTock();
        MyThread mt1 = new MyThread("Tick", tt);
        MyThread mt2 = new MyThread("Tock", tt);

        mt1.Thrd.Join();
        mt2.Thrd.Join();
        Console.WriteLine("Clock Stopped");

        Console.ReadKey();

    }
}


