// Demonstrate a continuation. 
//Продолжение и остановка потоков


using System;
using System.Threading;
using System.Threading.Tasks;

class ContinuationDemo
{

    // A method to be run as a task. 
    static void MyTask()
    {
        Console.WriteLine("MyTask() starting");

        for (int count = 0; count < 5; count++)
        {
            Thread.Sleep(500);
            Console.WriteLine("In MyTask() count is " + count);
        }

        Console.WriteLine("MyTask terminating");
    }

    // A method to be run as a continuation. 
    static void ContTask(Task t)
    {
        Console.WriteLine("Continuation starting");

        for (int count = 20; count < 25; count++)
        {
            Thread.Sleep(500);
            Console.WriteLine("Continuation count is " + count);
        }
        Console.WriteLine("Continuation terminating");
    }

    static void Main()
    {

        Console.WriteLine("Main thread starting.");

        // Construct the first task. 
        Task firstTsk = new Task(MyTask);
        Console.WriteLine("First task:"+firstTsk.Id);
        // Now, create the continuation. 
        #region Example 1
        //Task taskCont = tsk.ContinueWith(ContTask);//ContTask - задача запуститься после завершения taskCont
        #endregion
        #region Example 2. Using labmda for continuation
        Task taskCont = firstTsk.ContinueWith((prevTask) =>
        {
            Console.WriteLine("Continuation starting");
            Console.WriteLine("PrevTask ID:"+prevTask.Id);
            for (int count = 6; count <= 10; count++)
            {
                Thread.Sleep(500);
                Console.WriteLine("Continuation count is " + count);
            }
            Console.WriteLine("Continuation terminating");

        });
        #endregion
        // Begin the task sequence. 
        firstTsk.Start();

        //Закоментируйте и раскоментируюте следующие строчки. Будет ошибка. Почему?
        taskCont.Wait();//Ждем окончания только продолжения задачи
        //firstTsk.Wait();

         firstTsk.Dispose();
            taskCont.Dispose();

        Console.WriteLine("Main thread ending.");

        Console.ReadKey();

    }
}