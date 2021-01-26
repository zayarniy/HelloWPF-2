// A simple example of cancellation that use polling. 
/*
В версии 4.0 среды .NET Framework внедрена новая подсистема, обеспечивающая структурированный,
хотя и очень удобный способ отмены задачи. 
Эта новая подсистема основывается на понятии признака отмены. 
Признаки отмены поддерживаются в классе Task, среди прочего, с помощью фабричного метода StartNew ().
*/
using System;
using System.Threading;
using System.Threading.Tasks;

class DemoCancelTask
{

    // A method to be run as a task. 
    static void MyTask(Object ct)//передаем токен отмены
    {
        CancellationToken cancelTok = (CancellationToken)ct;

        // Check if cancelled prior to starting. 
        cancelTok.ThrowIfCancellationRequested();

        Console.WriteLine("MyTask() starting");

        for (int count = 0; count < 10; count++)
        {
            // This example uses polling to watch for cancellation. 
            if (cancelTok.IsCancellationRequested)
            {
                Console.WriteLine("Cancellation request received.");
                cancelTok.ThrowIfCancellationRequested();
            }
             
            Thread.Sleep(500);
            Console.WriteLine("In MyTask(), count is " + count);
        }

        Console.WriteLine("MyTask terminating");
    }

    static void Main()
    {

        Console.WriteLine("Main thread starting.");

        // Create a cancellation token source. 
        CancellationTokenSource cancelTokSrc = new CancellationTokenSource();

        // Start a task, passing the cancellation token to both 
        // the delegate and the task. 
        Task tsk = Task.Factory.StartNew(MyTask, cancelTokSrc.Token,
                                         cancelTokSrc.Token);

        // Let tsk run until cancelled. 
        Thread.Sleep(3000);

        try
        {
            // Cancel the task. 
            cancelTokSrc.Cancel();

            // Suspend Main() until tsk terminates. 
            tsk.Wait();
        }
        catch (AggregateException exc)
        {
            if (tsk.IsCanceled)
                Console.WriteLine("\ntsk Cancelled\n");

            // To see the exception, un-comment this line: 
            // Console.WriteLine(exc); 
        }
        finally
        {
            
            tsk.Dispose();
            //Must invoke Dispose for Token Source!
            cancelTokSrc.Dispose();
        }

        Console.WriteLine("Main thread ending.");

        Console.Read();

    }
}
/*
Признак отмены является экземпляром объекта типа CancellationToken, т.е. структуры, определенной в пространстве имен System. Threading. 
В структуре CancellationToken определено несколько свойств и методов, но мы воспользуемся двумя из них. 
Во-первых, это доступное только для чтения свойство IsCancellationRequested, которое объявляется следующим образом.
public bool IsCancellationRequested { get; }

Оно возвращает логическое значение true, если отмена задачи была запрошена для вызывающего признака, 
а иначе — логическое значение false. И во-вторых, это метод ThrowlfCancellationRequested (), 
который объявляется следующим образом
public void ThrowlfCancellationRequested()   
*/
