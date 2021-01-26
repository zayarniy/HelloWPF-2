// Use a Mutex. 
/*
 * Мьютекс представляет собой взаимно исключающий синхронизирующий объект. 
 * Это означает, что он может быть получен потоком только по очереди. 
 * Мьютекс предназначен для тех ситуаций, в которых общий ресурс может быть одновременно 
 * использован только в одном потоке. Допустим, что системный журнал совместно 
 * используется в нескольких процессах, но только в одном из них данные могут 
 * записываться в файл этого журнала в любой момент времени. 
 * Для синхронизации процессов в данной ситуации идеально подходит мьютекс.
 * */

using System;
using System.Threading;

// This class contains a shared resource (Count), 
// and a mutex (Mtx) to control access to it.  
// Класс содержит общий ресурс (Count) и мьютекс (Mtx) для контроля доступа к нему
class SharedRes
{
    public static int Count = 0;
    public static Mutex Mtx = new Mutex();
}

// This thread increments SharedRes.Count. 
// Этот поток увеличивает SharedRes.Count. 
class IncThread
{
    int num;
    public Thread Thrd;

    public IncThread(string name, int n)
    {
        Thrd = new Thread(this.Run);
        num = n;
        Thrd.Name = name;
        Thrd.Start();
    }

    // Entry point of thread.  
    void Run()
    {

        Console.WriteLine(Thrd.Name + " is waiting for the mutex.");

        // Acquire the Mutex. 
        //Uncomment this string for use Mutex
        SharedRes.Mtx.WaitOne();

        Console.WriteLine(Thrd.Name + " acquires the mutex.");

        do
        {
            Thread.Sleep(500);
            SharedRes.Count++;
            Console.WriteLine("In " + Thrd.Name +
                              ", SharedRes.Count is " + SharedRes.Count);
            num--;
        } while (num > 0);

        Console.WriteLine(Thrd.Name + " releases the mutex.");

        // Release the Mutex. 
        //Uncomment this string too
        SharedRes.Mtx.ReleaseMutex();
    }
}

// This thread decrements SharedRes.Count. 
// Этот поток уменьшает SharedRes.Count. 
class DecThread
{
    int num;
    public Thread Thrd;

    public DecThread(string name, int n)
    {
        Thrd = new Thread(new ThreadStart(this.Run));
        num = n;
        Thrd.Name = name;
        Thrd.Start();
    }

    // Entry point of thread.  
    void Run()
    {

        Console.WriteLine(Thrd.Name + " is waiting for the mutex.");

        // Acquire the Mutex. 
        //Uncomment this string for use Mutex (или закоментируй, чтобы показать гонку ресурсов)
        SharedRes.Mtx.WaitOne();//ждем освобождения мьютекса

        Console.WriteLine(Thrd.Name + " acquires the mutex.");

        do
        {
            Thread.Sleep(500);
            SharedRes.Count--;
            Console.WriteLine("In " + Thrd.Name +
                              ", SharedRes.Count is " + SharedRes.Count);
            num--;
        } while (num > 0);

        Console.WriteLine(Thrd.Name + " releases the mutex.");

        // Release the Mutex. 
        //Uncomment this string too
        SharedRes.Mtx.ReleaseMutex();//освобождаем мьютекс
    }
}

class MutexDemo
{
    static void Main()
    {

        // Construct three threads.  
        IncThread mt1 = new IncThread("Increment Thread", 5);

        Thread.Sleep(1); // let the Increment thread start 

        DecThread mt2 = new DecThread("Decrement Thread", 5);

        mt1.Thrd.Join();
        mt2.Thrd.Join();
        Console.WriteLine("Main thread is ending");
        Console.ReadKey();
    }
}