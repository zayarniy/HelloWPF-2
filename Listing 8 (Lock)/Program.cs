// Use lock to synchronize access to an object.  

//1. Запустить без блокировки - показать не правильный результат не равный 15
//2. Разкомментировать lock
using System;
using System.Threading;

class SumArray
{
    int sum;
    private object lockOn = new object(); // объект используемый как симофор блокировки

    public int SumIt(int[] nums)
    {
        lock (lockOn) // lock the entire method  - блокировка метода 
        {
            sum = 0; // reset sum  

            for (int i = 0; i < nums.Length; i++)
            {
                sum += nums[i];
                Console.WriteLine("Running total for " +
                       Thread.CurrentThread.Name +
                       " is " + sum);
                Thread.Sleep(10); // allow task-switch  
            }
            return sum;
        }
    }


    
    public int SumIt2(int[] nums)
    {
        bool _lockWasTacken = false;
        try // lock the entire method  - блокировка метода 
        {
            System.Threading.Monitor.Enter(lockOn, ref _lockWasTacken);
            /*
            Метод Monitor.Enter принимает два параметра - объект блокировки и значение типа bool, которое указывает на результат блокировки (если он равен true, то блокировка успешно выполнена). Фактически этот метод блокирует объект locker так же, как это делает оператор lock.
                        */
            sum = 0; // reset sum  

            for (int i = 0; i < nums.Length; i++)
            {
                sum += nums[i];
                Console.WriteLine("Running total for " +
                       Thread.CurrentThread.Name +
                       " is " + sum);
                Thread.Sleep(10); // allow task-switch  
            }            
            return sum;
        }
        finally
        {
            if (_lockWasTacken)
            {
                System.Threading.Monitor.Exit(lockOn);
                /*
                 С помощью А в блоке try...finally с помощью метода Monitor.Exit происходит освобождение объекта locker, если блокировка осуществлена успешно, и он становится доступным для других потоков.
                                */
            }
        }
    }
}

class MyThread
{
    public Thread Thrd;
    int[] a;
    int answer;

    // Create one SumArray object for all instances of MyThread. 
    static SumArray sa = new SumArray();

    // Construct a new thread.  
    public MyThread(string name, int[] nums)
    {
        a = nums;
        Thrd = new Thread(this.Run);
        Thrd.Name = name;
        Thrd.Start(); // start the thread  
    }

    // Begin execution of new thread.  
    void Run()
    {
        Console.WriteLine(Thrd.Name + " starting.");

        answer = sa.SumIt(a);

        Console.WriteLine("Sum for " + Thrd.Name +
                           " is " + answer);

        Console.WriteLine(Thrd.Name + " terminating.");
    }
}

class Sync
{
    static void Main()
    {
        int[] a = { 1, 2, 3, 4, 5 };

        MyThread mt1 = new MyThread("Child #1", a);
        MyThread mt2 = new MyThread("Child #2", a);
        //Запускаем оба потока
        mt1.Thrd.Join();
        mt2.Thrd.Join();

        Console.Read();

    }
}
/*
 * Ниже подведены краткие итоги использования блокировки.
•	Если блокировка любого заданного объекта получена в одном потоке, то после блокировки объекта она не может быть получена в другом потоке.
•	Остальным потокам, пытающимся получить блокировку того же самого объекта, придется ждать до тех пор, пока объект не окажется в разблокированном состоянии.
•	Когда поток выходит из заблокированного фрагмента кода, соответствующий объект разблокируется.

*/