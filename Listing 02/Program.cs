// Use an instance method as a task. 
//Простой пример создания задачи
//Использование не статического метода
using System;
using System.Threading;
using System.Threading.Tasks;

class MyClass
{

    public string Name = "Noname";

    // A method to be run as a task. 
    public void MyTask()
    {
        System.Threading.Thread.CurrentThread.Name = Name;
        Console.WriteLine($"MyTask({Name}) starting");

        for (int count = 0; count < 10; count++)
        {
            Thread.Sleep(500);
            Console.WriteLine("In MyTask(), count is " + count);
        }

        Console.WriteLine("MyTask terminating");
    }
}

class DemoTask
{

    static void Main()
    {

        Console.WriteLine("Main thread starting.");

        // Construct a MyClass object. 
        MyClass mc = new MyClass() { Name = "Task1" };

        // Construct a task on mc.MyTask(). 
        Task tsk = new Task(mc.MyTask);

        // Run the task. 
        tsk.Start();

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
Результат выполнения этой программы получается таким же, как и прежде. Единственное отличие состоит в том, что метод MyTask () вызывается теперь для экземпляра объекта класса MyClass.
*/
/*
В отношении задач необходимо также иметь в виду следующее: после того, как за дача завершена, она не может быть перезапущена. Следовательно, иного способа по вторного запуска задачи на исполнение, кроме создания ее снова, не существует.
*/