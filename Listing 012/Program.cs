﻿// Show timing differences between sequential and parallel for loops. 

using System;
using System.Threading.Tasks;
using System.Diagnostics;

//Важно понимать, что не всегда распораллеливание данных будет приводить к увеличению производительности
class DemoParallelFor
{
    static int[] data;
    const int Size = 20000000;//Size of data (при маленьких значениях, видно, что параллельные вычисления делаются дольше

    // A method to be run as the body of a parallel loop. 
    // The statements in this loop are designed to simply 
    // consume some CPU time for the puposes of demonstration. 
    static void MyTransform(int i)
    {
        // So something for demonstration purposes. 
         
        data[i] = data[i] / 10;

        if (data[i] < 1000) data[i] = 0;
        if (data[i] > 1000 & data[i] < 2000) data[i] = 100;
        if (data[i] > 2000 & data[i] < 3000) data[i] = 200;
        if (data[i] > 3000) data[i] = 300;
    }

    static void Main()
    {

        Console.WriteLine("Main thread starting.");

        // Create a Stopwatch instance to time loops. 
        Stopwatch sw = new Stopwatch();

        data = new int[Size];

        // Initialize data. 
        sw.Start();

        // Parallel version of initialization loop. 
        Parallel.For(0, data.Length, (i) => data[i] = i);

        sw.Stop();

        Console.WriteLine("Parallel intialization loop:    {0} secs",
                           sw.Elapsed.TotalSeconds);

        sw.Reset();

        sw.Start();

        // Sequential version of initialization loop. 
        for (int i = 0; i < data.Length; i++) data[i] = i;

        sw.Stop();

        Console.WriteLine("Sequential initialization loop: {0} secs",
                           sw.Elapsed.TotalSeconds);
        Console.WriteLine();

        // Perform transforms. 

        sw.Start();

        // Parallel version of transformation loop. 
        Parallel.For(0, data.Length, MyTransform);

        sw.Stop();

        Console.WriteLine("Parallel transform loop:   {0} secs",
                           sw.Elapsed.TotalSeconds);

        sw.Reset();

        sw.Start();

        // Sequential version of transformation loop. 
        for (int i = 0; i < data.Length; i++) MyTransform(i);

        sw.Stop();

        Console.WriteLine("Sequential transform loop: {0} secs",
                           sw.Elapsed.TotalSeconds);

        Console.WriteLine("Main thread ending.");

        Console.Read();

    }
}
