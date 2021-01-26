// Create and run a task. 
// 1 способ создание и запуск потока
using System;
using System.Threading;
using System.Threading.Tasks;

class DemoTask
{
     
    // A method to be run as a task. 
    static void MyTask()
    {
        Console.WriteLine("MyTask() starting");

        //Выполняет работу в потоке
        for (int count = 0; count < 10; count++)
        {
            Thread.Sleep(500);
            Console.WriteLine("In MyTask(), count is " + count);
        }

        Console.WriteLine("MyTask terminating");
    }

    static void Main()
    {

        Console.WriteLine("Main thread starting.");

        // Construct a task. 
        //Создаем задачу
        Task tsk = new Task(MyTask);//MyTask - точка входа в исполнение потока

        // Run the task. 
        tsk.Start();//запускаем задачу в отдельном потоке

        // Keep Main() alive until MyTask() finishes. 
        for (int i = 0; i < 60; i++)
        {
            Console.Write(".");
            Thread.Sleep(100);
        }

        Console.WriteLine("Main thread ending.");

        Console.Read();

    }
}
/*
Следует иметь в виду, что по умолчанию задача исполняется в фоновом потоке. Следовательно, при завершении создающего потока завершается и сама задача. Именно поэтому в рассматриваемой здесь программе метод Thread .Sleep() использован для сохранения активным основного потока до тех пор, пока не завершится выполнение метода MyTask (). Как и следовало ожидать, организовать ожидание завершения задачи можно и более совершенными способами, что и будет показано далее.
*/
