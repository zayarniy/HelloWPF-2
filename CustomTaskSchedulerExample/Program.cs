using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//Пример использования TaskScheduler
//https://www.infoworld.com/article/3063560/how-to-build-your-own-task-scheduler-in-csharp.html

namespace CustomTaskSchedulerExample
{
    class Program
    {
        static void SomeMethod()
        {

        }

        static void Main(string[] args)
        {
            CustomTaskScheduler taskScheduler = new CustomTaskScheduler();
            Task.Factory.StartNew(() => SomeMethod(), CancellationToken.None, TaskCreationOptions.None, taskScheduler);
        }
    }
}
