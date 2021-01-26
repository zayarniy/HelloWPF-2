// Use Parallel.Invoke() to execute methods concurrently. 
using System;
using System.Threading;
using System.Threading.Tasks;

class DemoParallel
{

    // A method to be run as a task. 
    static void MyMeth()
    {
        Console.WriteLine("MyMeth starting");

        for (int count = 0; count < 5; count++)
        {
            Thread.Sleep(500);
            Console.WriteLine("In MyMeth, count is " + count);
        }

        Console.WriteLine("MyMeth terminating");
    }

    // A method to be run as a task. 
    static void MyMeth2()
    {
        Console.WriteLine("MyMeth2 starting");

        for (int count = 0; count < 5; count++)
        {
            Thread.Sleep(500);
            Console.WriteLine("In MyMeth2, count is " + count);
        }

        Console.WriteLine("MyMeth2 terminating");
    }

    static void Main()
    {
        Console.WriteLine("Main thread starting.");
        // Run two named methods. 
        Parallel.Invoke(MyMeth, MyMeth2);
        Console.WriteLine("Main thread ending.");
        //Console.Read();
    }
}
/*
 Метод Invoke (), определенный в классе Parallel, позволяет выполнять один или несколько методов, указываемых в виде его аргументов. Он также масштабирует исполнение кода, используя доступные процессоры, если имеется такая возможность. Ниже приведена простейшая форма его объявления.
public static void Invoke(params Action[] actions)
Выполняемые методы должны быть совместимы с описанным ранее делегатом Action. 
Напомним, что делегат Action объявляется следующим образом.

public delegate void Action()

Следовательно, каждый метод, передаваемый методу Invoke () в качестве аргумента, не должен ни принимать параметров, ни возвращать значение. Благодаря тому что параметр actions данного метода относится к типу params, выполняемые методы могут быть указаны в виде переменного списка аргументов. Для этой цели можно также воспользоваться массивом объектов типа Action, но зачастую оказывается проще указать список аргументов.
Метод Invoke () сначала инициирует выполнение, а затем ожидает завершения всех передаваемых ему методов. Это, в частности, избавляет от необходимости (да и не позволяет) вызывать метод Wait (). Все функции параллельного выполнения метод Wait () берет на себя. И хотя это не гарантирует, что методы будут действительно выполняться параллельно, тем не менее, именно такое их выполнение предполагается, если система поддерживает несколько процессоров. Кроме того, отсутствует возможность указать порядок выполнения методов от первого и до последнего, и этот порядок не может быть таким же, как и в списке аргументов.* 
*/
