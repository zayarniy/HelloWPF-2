using System;
using System.Threading;

//Join позволяет контролировать выполнение потоков дожидаясь выполнения
//определенного потока

public class Example
{
    static Thread thread1, thread2;

    public static void Main()
    {
        thread1 = new Thread(ThreadProc);
        thread1.Name = "Thread1";
        thread1.Start();

        thread2 = new Thread(ThreadProc);
        thread2.Name = "Thread2";
        thread2.Start();
        Console.WriteLine("Основной поток не завершится пока не завершатся все остальные потоки внутри процесса");
        Console.ReadKey();

    }

    private static void ThreadProc()
    {
        //Thread.CurrentThread.Name - позволяет получить имя потока выполнения
        Console.WriteLine("\nCurrent thread: {0}", Thread.CurrentThread.Name);
        //Если поток выполнения Thread1 и thread2 еще не запущен, то ждем завершения thread2
        if (Thread.CurrentThread.Name == "Thread1" &&
            thread2.ThreadState != ThreadState.Unstarted)
        {
            thread2.Join();
            Console.WriteLine("Join thread2");
            Console.WriteLine("Join позволяет контролировать выполнение потоков дожидаясь выполнения определенного потока");
        }

        Thread.Sleep(4000);
        Console.WriteLine("\nCurrent thread: {0}", Thread.CurrentThread.Name);
        Console.WriteLine("Thread1: {0}", thread1.ThreadState);
        Console.WriteLine("Thread2: {0}\n", thread2.ThreadState);


    }
}

/*
 *  Предупреждение

Никогда не следует вызывать метод Join объекта Thread, который представляет текущий 
поток из текущего потока. Это приведет к тому, что ваше приложение перестанет отвечать, 
поскольку текущий поток ждет, пока не исключается.
Этот метод изменяет состояние вызывающего потока для включения 
ThreadState.WaitSleepJoin. Нельзя вызвать Join в потоке, 
который находится в состоянии ThreadState.Unstarted.
*/
