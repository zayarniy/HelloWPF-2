// Return a value from a task. 
/*
Задача может возвращать значение. Это очень удобно по двум причинам. 
Во-первых, это означает, что с помощью задачи можно вычислить некоторый результат. 
Подобным образом поддерживаются параллельные вычисления. 
И во-вторых, вызывающий процесс окажется блокированным до тех пор, пока не будет получен результат. 
Это означает, что для организации ожидания результата не требуется никакой особой синхронизации. 
*/
using System;
using System.Threading;
using System.Threading.Tasks;

class DemoTask
{

    // A trivial method that returns a result and takes no arguments. 
    static bool MyTask()
    {
        Console.WriteLine("Wait 6 seconds");
        Thread.Sleep(6000);
        return true;
    }

    // This method returns the summation of a positive integer 
    // which is passed to it. 
    static int SumIt(object v)
    { 
        Console.WriteLine("Wait 7 seconds");
        Thread.Sleep(7000);
        int x = (int)v;
        int sum = 0;

        for (; x > 0; x--)
            sum += x;

        return sum;
    }

    static void Main()
    {

        Console.WriteLine("Main thread starting.");

        // Construct the first task. 
        //1 пример
        Task<bool> tsk = Task<bool>.Factory.StartNew(MyTask);
        Task<bool> tsk1 = new Task<bool>(MyTask);
        tsk1.Start();

        //вызывающий процесс окажется блокированным до тех пор, пока не будет получен результат. 
        Console.WriteLine("After running MyTask. The result is " +
                          tsk.Result);

        // Construct the second task. 
        //Второй пример
        Task<int> tsk2 = Task<int>.Factory.StartNew(SumIt, 3);//Создаем задачу, которая возвращает int

        //Task<int> tsk3 = new Task<int>(SumIt, 3);
        Task<int> tsk3 = new Task<int>(new Func<object,int>(SumIt), 3);
        tsk3.Start();

        //вызывающий процесс окажется блокированным до тех пор, пока не будет получен результат. 
        
        Console.WriteLine("After running SumIt. The result is " +
                          tsk2.Result);

        tsk.Dispose();
        tsk2.Dispose();

        Console.WriteLine("Main thread ending.");

        Console.ReadKey();

    }
}