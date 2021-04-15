using System;
using System.Threading;

namespace Example010
{
    class Example01
    {
        public static void Run()
        {

            Thread thread = new Thread(new ThreadStart(ThreadMethod));
            thread.Name = "Вторичный поток";
            thread.Start();
            Console.WriteLine("Ждем окончания работы потока.");
        }


        static void ThreadMethod()
        {
            Thread.Sleep(2000);
            Console.WriteLine($"{Thread.CurrentThread.Name} завершен.");
        }
    }

    //Чтобы передавать в поток параметры, используют делегат ParameterizedThreadStart. 
    class Example02
    {

        //// Величина приостановки потока
        public static void Run(int sleepTime = 2000)
        {
            
            Thread thread = new Thread(new ParameterizedThreadStart(ThreadMethod));
            thread.Name = "Вторичный поток";
            thread.Start(sleepTime);//Тогда используется перегрузка Start, которая будет принимать параметр типа object

            Console.WriteLine("Ждем окончания работы потока.");

            Console.WriteLine("Ждем окончания работы потока.");
            Console.ReadKey();
        }

        static void ThreadMethod(object sleepTime)
        {
            Thread.Sleep((int)sleepTime);
            Console.WriteLine($"{Thread.CurrentThread.Name} завершен.");
        }

    }

    //Передача множества данных
    class Example03
    {
        //Отдельный класс для передачи данных в другой поток
        public class ThreadParam
        {
            public int SleepTime { get; set; }
            public string Message { get; set; }
        }


        //// Величина приостановки потока
        public static void Run()
        {

            //Создаем объект для передачи данных в другой поток
            ThreadParam threadClass = new ThreadParam
            {
                SleepTime = 2000,
                Message = "Сообщение из одного потока другому: SOS"
            };
            //Создаем поток с использованием делегата для передачи данных и указываем параметризированный метод
            Thread thread = new Thread(new ParameterizedThreadStart(ThreadMethod))
            {
                Priority = ThreadPriority.Highest,
                Name = "Вторичный поток"
            };
            #region Правильный код
            //Стартуем поток, передав класс с параметрами
            //thread.Start(threadClass);
            #endregion

            thread.Start("Любой отличный от threadClass тип данных");//Будет ошибка если раскоментировать
            #region Пример ошибки с динамическим типом данных
            dynamic param = new
            {
                SleepTime = 2000,
                Message = "It is a dynamic type"
            };
            //thread.Start(param);//Будет ошибка если раскоментировать
            Console.WriteLine("Ждем окончания работы потока.");
        }
        #endregion
        //Параметризированный метод
        static void ThreadMethod(object obj)
        {
            //Приводим данные к нужному типу
            ThreadParam threadClass = (ThreadParam)obj;
            Console.WriteLine(threadClass.Message);
            Thread.Sleep(threadClass.SleepTime);
        }

    }


    //Передаем ее поток, но есть ограничение: метод Thread.Start() небезопасен к типам, 
    //поэтому мы вынуждены приводить полученный объект к необходимому типу:
    //ThreadClass threadClass = (ThreadClass)obj;
    //Решение — объявить все методы и переменные нового потока в одном классе, 
    //а сам поток запускать через делегат

    class Example04
    {
        //Поток с параметрами
        public class ThreadClass
        {
            private int _sleepTime;
            private string _message;
            public ThreadClass(int sleepTime, string message)
            {
                _sleepTime = sleepTime;
                _message = message;
            }

            //Метод входа в поток
            public void ThreadMethod()
            {
                Thread.Sleep(_sleepTime);
                Console.WriteLine(_message);
            }

        }


        //// Величина приостановки потока
        public static void Run()
        {

            ThreadClass threadClass = new ThreadClass(2000, "Поток завершен.");
            Thread thread = new Thread(new ThreadStart(threadClass.ThreadMethod));
            thread.Priority = ThreadPriority.Highest;
            thread.Name = "Вторичный поток";
            thread.Start();
            Console.WriteLine("Ждем окончания работы потока.");
            thread.Join();
        }


    }

    

    class Program
    {
        static void Main()
        {
            //Example01.Run();//Пример потока
            //Example02.Run(3000);//Пример передачи параметров в поток
            Example03.Run();//Передача множества данных 
            //Example04.Run();//Решение проблемы с типовой безопасностью при передаче данных
            Console.WriteLine("Основной поток завершен. Нажмите любую клавишу");
          Console.ReadKey();//Пока хотя бы один поток активен процесс не завершится
        }
    }
}
