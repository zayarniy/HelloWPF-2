using System;
using System.Runtime.Remoting.Contexts;
using System.Threading;

namespace Example020_ThreadsSync
{
#region Пример 1. Синхронизация через lock
    //В данном примере воссоздана ситуация, когда несколько потоков будут стремиться получить доступ к одному ресурсу(переменной)
    //одновременно, что, без дополнительного вмешательства, будет приводить к неверному результату
    class Example01
    {
        //переменная-ключ блокировки
        private static object lockObject = new object();
        static int sum = 0;//общий ресурс
        public static void Run()
        {            
            for (int i = 0; i < 5; i++)
            {
                Thread thread = new Thread(ThreadMethod) { Name = i.ToString() };
                thread.Start();
                //Thread.Sleep(100);//После запуска каждого процесса, чтобы дождаться его выполнения
            }
            Console.ReadKey();
        }

        static void ThreadMethod()
        {
            lock (lockObject) //Закоментируй, чтобы показать использование общего ресурса
            {                
                sum = 0;//Итоговая правильная сумма 45
                for (int i = 0; i < 10; i++)
                {
                    sum+=i;//без блокировки все одновременно будут менять значение переменной, 
                    //не дожидаясь окончания завершения выполнения блока
                    Thread.Sleep(10);//В зависимости от задержки и без lock будут разные значения
                }
                Console.WriteLine("Поток {0}: {1}", Thread.CurrentThread.Name, sum);
            }
        }

    }
    #endregion
    #region  Пример 2. Атрибут [Synchronization]    
    [Synchronization]//Закоментируй, чтобы продемонстрировать разницу
    public class ClassWithSynchronizationAttribute : ContextBoundObject
    {
        private int _value;
        public void ThreadMethod()
        {
            Console.WriteLine("  Поток {0}: _value = {1}", Thread.CurrentThread.Name, _value);
            _value++;
            Thread.Sleep(1000);
        }
    }
    class Example02
    {
        public static void Run()
        {
            ClassWithSynchronizationAttribute cwsa = new ClassWithSynchronizationAttribute();
            for (int i = 0; i < 10; i++)
            {
                //Передаем в поток общий для всех метод и с общей для всех потоков переменной
                Thread thread = new Thread(cwsa.ThreadMethod) { Name = "Поток #"+i };
                thread.Start();
            }


        }

        //Этот подход легок в применении, но использовать его надо с осторожностью. Члены класса будут блокироваться в любом случае, необходимо это или нет, что может негативно отразиться на производительности.

    }
    #endregion

    //ThreadPool - предоставляет пул потоков, который можно использовать для выполнения задач, 
    //отправки рабочих элементов, обработки асинхронного ввода-вывода, 
      //ожидания от имени других потоков и обработки таймеров.
    //Здесь пример с потоками слегка модифицирован. ThreadPool.QueueUserWorkltem() помещает метод в очередь на выполнение. 
    //Выполнение осуществляет доступный поток, который берется из пула потоков.

    #region 3. ThreadPool
    class Example03
    {
        private static object lockObject = new object();

        class Param
        {
            public string Name { get; set; }
        }

        static public void Run()
        {
            ThreadPool.SetMinThreads(2, 2);
            ThreadPool.SetMaxThreads(5, 5);

            for (int i = 0; i < 5; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadMethod), new Param() { Name = "Thread #" + i });
            }
        }
        static void ThreadMethod(object state)
        {
            Param param = (Param)state;
            Console.WriteLine("Call "+param.Name);
            lock (lockObject)
            {
                int value = 0;

                for (int i = 0; i < 5; i++)
                {
                    //Console.WriteLine("Поток {0}: {1}", Thread.CurrentThread.Name, value);
                    Console.WriteLine("Поток {0}: {1}", param.Name, value);
                    value++;
                    Thread.Sleep(200);
                }
            }
        }


    }
    #endregion



    class Program
    {
        static void Main()
        {
            //Example01.Run();
            //Example02.Run();
            Example03.Run();
            Console.WriteLine("Завершение основного потока");
            Console.ReadKey();
        }
    }
}
