using System;
using System.Runtime.CompilerServices;//Synchronized
using System.Threading;

//**** Пока не показывать ****
namespace ThreadPoolExample
{
    public class Printer
    {
        public string Name { get; set; }
        object obj = new object();

        [MethodImplAttribute(MethodImplOptions.Synchronized)]
        public void PrintNumbers()
        {
            //lock (obj)//закоментировать для демонстрации гонки
            {
                Console.WriteLine("Printing");
                for (int i = 0; i < 10; i++)
                {
                    Console.Write(i);
                    Thread.Sleep(100);
                }
                Console.WriteLine();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int CountofWorkThreads; // Переменная для хранения значения максимального количества потоков в пуле.
            int CountofInputOutputThreads; // Переменная для хранения количества потоков ввода-вывода в пуле.
            ThreadPool.GetMaxThreads(out CountofWorkThreads, out CountofInputOutputThreads); // Инициируем переменные.
            Console.WriteLine("Максимальное количество потоков: " + CountofWorkThreads + "\nКоличество потоков ввода-вывода: " + CountofInputOutputThreads); // Выводим значения на консоль.

            Console.WriteLine(" * ****CLR Thread Pool * ****\n");
            Console.WriteLine("Main thread started. ThreadID = {0}",
            Thread.CurrentThread.ManagedThreadId);
            

            Printer p = new Printer();
            WaitCallback workItem = new WaitCallback(PrintTheNumbers);
            WaitCallback workItem2 = new WaitCallback(PrintTheNumbers);               
            ThreadPool.QueueUserWorkItem(workItem, p);
            ThreadPool.QueueUserWorkItem(workItem2, p);
            ThreadPool.QueueUserWorkItem(workItem2, p);
            ThreadPool.QueueUserWorkItem(workItem, p);
            ThreadPool.QueueUserWorkItem(workItem, p);
            Console.WriteLine("All tasks queued");
            Thread.Sleep(10000);
            Console.WriteLine("Main thread ending");
            //Console.ReadLine();
        }

        static void PrintTheNumbers(object state)
        {
            Printer task = (Printer)state; 
            task.PrintNumbers();
        }
    }
}
/*
Рекомендации по использованию
Пул потоков никогда не бывает приоритетным, поэтому все находящиеся в нем потоки – фоновые и будут завершены, как только завершатся главные потоки (с высоким приоритетом). Программисту необходимо за этим следить. Как следствие потоки в пуле не имеют приоритета и идентифицирующего имени, они не именные, а носят роль обслуживающих.

Прервать работу потока, который запускается из пула нельзя, как нельзя его идентифицировать.

Пул потоков – это автоматическое средство для задач, которые требуют временных запусков потоков. Если потоки должны быть активны все время существования приложения, лучше воспользоваться функциональностью класса Thread и реализовать взаимодействие между потоками другими средствами 
https://brainoteka.com/blogs/c-spravochnik/pul-potokov
*/