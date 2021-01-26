// Use Interlocked operations. 

/*
 * Еще одним классом, связанным с синхронизацией, является класс Interlocked.
 * Этот класс служит в качестве альтернативы другим средствам синхронизации, 
 * когда требуется только изменить значение общей переменной. 
 * Методы, доступные в классе Interlocked, гарантируют, что их действие будет выполняться как единая, 
 * непрерываемая операция. Это означает, что никакой синхронизации в данном случае вообще не требуется. 
 * В классе Interlocked предоставляются статические методы для сложения двух целых значений, 
 * инкрементирования и декрементирования целого значения, сравнения и установки значений объекта, 
 * обмена объектами и получения 64-разрядного значения. Все эти операции выполняются без прерывания.
 * */
using System;
using System.Threading;

// A shared resource. 
class SharedRes
{
    public static int Count = 0;
}

// This thread increments SharedRes.Count. 
class IncThread
{
    public Thread Thrd;

    public IncThread(string name)
    {
        Thrd = new Thread(this.Run);
        Thrd.Name = name;
        Thrd.Start();
    }

    // Entry point of thread.  
    void Run()
    {

        for (int i = 0; i < 5; i++)
        {
            Interlocked.Increment(ref SharedRes.Count);
            //SharedRes.Count++;
            Console.WriteLine(Thrd.Name + " Count is " + SharedRes.Count);
        }
    }
}

// This thread decrements SharedRes.Count. 
class DecThread
{

    public Thread Thrd;

    public DecThread(string name)
    {
        Thrd = new Thread(this.Run);
        Thrd.Name = name;
        Thrd.Start();
    }

    // Entry point of thread.  
    void Run()
    {

        for (int i = 0; i < 5; i++)
        {
            Interlocked.Decrement(ref SharedRes.Count);
            //SharedRes.Count--;
            Console.WriteLine(Thrd.Name + " Count is " + SharedRes.Count);
        }
    }
}

class InterdlockedDemo
{
    static void Main()
    {

        // Construct two threads.  
        IncThread mt1 = new IncThread("Increment Thread");
        DecThread mt2 = new DecThread("Decrement Thread");

        mt1.Thrd.Join();
        mt2.Thrd.Join();

        Console.Read();

    }
}