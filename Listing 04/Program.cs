// Use Wait(). 

using System;
using System.Threading;
using System.Threading.Tasks;
//Использование Wait для ожидания окончания завершения задачи
class DemoTask
{

    // A method to be run as a task. 
    static void MyTask()
    {
        Console.WriteLine("MyTask() #" + Task.CurrentId + " starting");

        for (int count = 0; count < 10; count++)
        {
            Thread.Sleep(500);
            Console.WriteLine("In MyTask() #" + Task.CurrentId +
                              ", count is " + count);
        }

        Console.WriteLine("MyTask #" + Task.CurrentId + " terminating");
    }

    static void Main()
    {

        Console.WriteLine("Main thread starting.");

        // Construct two tasks. 
        Task tsk = new Task(MyTask);
        Task tsk2 = new Task(MyTask);
         
        // Run the tasks. 
        tsk.Start();
        tsk2.Start();

        Console.WriteLine("Task ID for tsk is " + tsk.Id);
        Console.WriteLine("Task ID for tsk2 is " + tsk2.Id);

        // Suspend Main() until both tsk and tsk2 finish. 
        //tsk.Wait();
        //tsk2.Wait();
        //Task.WaitAll(tsk,tsk2);//Этой командой можно заменить обе задачи
        //Task.WaitAny(tsk, tsk2);//Ожидать пока не завершится какая-нибудь из задач
        int howIsFirst =Task.WaitAny(tsk, tsk2);////Ожидаем завершения любого из потоков, Index - вернет номер потока, который завершился первым
        Console.WriteLine("First ending is task #"+(howIsFirst+1));
        
        Console.WriteLine("Main thread ending.");

        Console.ReadKey();

    }
}
//Wait() - приостанавливает исполнение вызывающего потока(Main) до тех пор, пока не завершится вызываемая задача
//WaitAll() - приостанавливает исполнение вызывающего потока(Main) до тех пор, пока не завершатся все задачи указанные в качестве парамметров метода
//Организуя ожидание завершения нескольких задач, следует быть особенно внимательным, чтобы избежать взаимоблокировок. Так, если две задачи ожидают завершения друг друга, то вызов метода WaitAll() вообще не приведет к возврату из него.

//WaitAny() - приостанавливает исполнение вызывающего потока(Main) до тех пор, пока не завершится любая из задач