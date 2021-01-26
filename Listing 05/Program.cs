﻿// TaskFactory, Use a lambda expression as a task and Dispose without wait (exception)
//Более совершенный пример запуска задач.
//Использование класса TaskFactory

using System;
using System.Threading;
using System.Threading.Tasks;

class DemoLambdaTask
{

    static void Main()
    {

        Console.WriteLine("Main thread starting.");

        // The following use a lambda expression to define a task. 
        Task tsk = Task.Factory.StartNew(delegate () {
            Console.WriteLine("Task #{0} starting", Task.CurrentId);

            for (int count = 0; count < 10; count++)
            {
                Thread.Sleep(500);
                Console.WriteLine($"Task #{Task.CurrentId} count is " + count);
            }

            Console.WriteLine("Task terminating");
        });

        // The following use a lambda expression to define a task. 
        #region Example 1
        Task tsk3 = Task.Factory.StartNew(Work);
        #endregion
         
        #region Example 2
        Task tsk2 = Task.Factory.StartNew(() => {
            Console.WriteLine("Task #{0} starting",Task.CurrentId);

            for (int count = 10; count < 50; count++)
            {
                Thread.Sleep(500);
                Console.WriteLine($"Task #{Task.CurrentId} count is " + count);
            }

            Console.WriteLine("Task terminating");
        });
        #endregion

        // Wait until tsk finishes.  
        //tsk.Wait();
        //tsk2.Wait();
        for(int i=0;i<10;i++)
        {
            Console.WriteLine("Main:"+i);
            Thread.Sleep(1000);
        }
        Task.WaitAll(tsk, tsk2, tsk3);
        // Dispose of tsk. 
        tsk.Dispose();//Это не обязательно для простых задач, но желательно, для объемных
        //tsk2.Dispose();//Exception if tsk2 won't finished
        Console.WriteLine("Main thread ending.");

       //Console.Read();

    }

    static void Work()
    {
        Console.WriteLine("Task #{0} starting", Task.CurrentId);

        for (int count = 10; count < 50; count++)
        {
            Thread.Sleep(500);
            Console.WriteLine($"Task #{Task.CurrentId} count is " + count);
        }

        Console.WriteLine("Task terminating");
    }
}
