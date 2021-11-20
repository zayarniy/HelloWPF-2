// Use a Semaphore 
// Ограничивает число потоков, которые могут одновременно обращаться к ресурсу или пулу ресурсов.
/*
Используйте Semaphore класс для управления доступом к пулу ресурсов. 
Потоки вводят семафор, вызывая WaitOne метод, который наследуется от WaitHandle класса, 
и освобождает семафор, вызывая Release метод.
Счетчик для семафора уменьшается каждый раз, когда поток входит в семафор, 
и увеличивается, когда поток освобождает семафор. Если значение счетчика равно нулю, 
последующие запросы блокируются до освобождения семафора другими потоками. 
Когда семафор освобожден всеми потоками, счетчик будет иметь максимальное значение, указанное при создании семафора.
Нет гарантированного порядка, например FIFO или ЛИФО, в котором заблокированные потоки вводят семафор.
*/
using System;
using System.Threading;

// This thread allows only two instances of itself 
// to run at any one time. 
class MyThread
{
    public Thread Thrd;

    // This creates a semaphore that allows up to 2 
    // permits to be granted and that initially has 
    // two permits available. 
    static Semaphore sem = new Semaphore(2, 2);
    /*Его конструктор принимает два параметра: первый указывает, какому числу объектов изначально будет доступен семафор, 
     * а второй параметр указывает, какой максимальное число объектов будет использовать данный семафор.
     * В данном случае у нас только два объекта могут одновременно находиться в библиотеке, 
     * поэтому максимальное число равно 2.
    */
    public MyThread(string name)
    {
        Thrd = new Thread(this.Run);
        Thrd.Name = name;
        Thrd.Start();
        Console.WriteLine("Поток "+name+" создан");
        //Thread.Sleep(100);
    }

    // Entry point of thread.  
    void Run()
    {
        Thread.Sleep(1000);
        //Имеем возможность делать работу только при получении разрешения
        Console.WriteLine(Thrd.Name + " ожидает разрешения.");

        sem.WaitOne();

        Console.WriteLine(Thrd.Name + " захватили использование ресурса.");

        for (char ch = 'A'; ch < 'D'; ch++)
        {
            Console.WriteLine(Thrd.Name + " : " + ch + " ");
            Thread.Sleep(3000);
        }

        Console.WriteLine(Thrd.Name + " разрешили использовать ресурс.");

        // Release the semaphore. 
        sem.Release();
    }
}


class SemaphoreDemo
{
    static void Main()
    {

        // Construct five threads.  
        // Создаем пять потоков
        const int N = 10;
        MyThread[] threads = new MyThread[N];
        for (int i = 0; i < N; i++) 
            threads[i] = new MyThread("Thread #"+i);

        foreach(MyThread thread in threads)
          thread.Thrd.Join();
        Console.WriteLine("Main thread ending");
        Console.Read();

    }
}