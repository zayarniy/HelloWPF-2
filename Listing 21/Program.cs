// Starting a new process. 

using System;
using System.Diagnostics;

class StartProcess
{
    static void Main()
    {
        Process newProc = Process.Start("wordpad.exe");
       

        Console.WriteLine("New process started.");

        newProc.WaitForExit();

        newProc.Close(); // free resources 

        Console.WriteLine("New process ended.");

        Console.ReadKey();

    }
}