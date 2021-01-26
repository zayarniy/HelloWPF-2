// Use Parallel.Invoke() to execute methods concurrently. C# 4.0
// This version uses lambda expressions. 

using System;
using System.Threading;
using System.Threading.Tasks;

class DemoParallel
{

    static void Main()
    {

        Console.WriteLine("Main thread starting.");

        // Run two anonymous methods specified via lambda expressions. 
        #region Example 1
        Parallel.Invoke(() =>
        {
            Console.WriteLine("Expression #1 starting");

            for (int count = 0; count < 5; count++)
            {
                Thread.Sleep(500);
                Console.WriteLine("Expression #1 count is " + count);
            }

            Console.WriteLine("Expression #1 terminating");
        },

            (Action)delegate ()
            {
                Console.WriteLine("Expression #2 starting");

                for (int count = 0; count < 5; count++)
                {
                    Thread.Sleep(500);
                    Console.WriteLine("Expression #2 count is " + count);
                }

                Console.WriteLine("Expression #2 terminating");
            }
        );
        #endregion
         
        #region Example 2 (more simple)
        //ParallelOptions parallel = new ParallelOptions();
        Parallel.Invoke(Task1, Task2);
        #endregion
        Console.WriteLine("Main thread ending.");

        Console.Read();

    }

    static void Task1()
    {
        Console.WriteLine("Expression #1 starting");

        for (int count = 0; count < 5; count++)
        {
            Thread.Sleep(500);
            Console.WriteLine("Expression #1 count is " + count);
        }

        Console.WriteLine("Expression #1 terminating");
    }

    static void Task2()
    {
        Console.WriteLine("Expression #2 starting");

        for (int count = 0; count < 5; count++)
        {
            Thread.Sleep(500);
            Console.WriteLine("Expression #2 count is " + count);
        }

        Console.WriteLine("Expression #2 terminating");

    }
}