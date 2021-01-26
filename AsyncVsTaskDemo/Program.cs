//https://andrey.moveax.ru/post/csharp-async-vs-task-and-parallel
//Пример сравнивающий три подхода использования потоков: 
//класс Parallel
//класс Task
//async,await
//Хорошо видно, что async/await работают быстрее, потому что не создают новых потоков
namespace AsyncVsTaskDemo
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal class TestMethods
    {
        private const int _maxTasks = 100;
        private readonly object _lock = new object();
        //список ID созданных потоков (чтобы отследить их кол-во)
        private readonly List<int> _loggedThreads = new List<int>();

        private void LongRunningTask(int i)
        {
            this.LogCurrentThread();
            //А в роли задачи, которая занимает время процессора, выступит Thread.Sleep().
            Thread.Sleep(100);
        }

        private async Task LongRunningAsync()
        {
            this.LogCurrentThread();
            //Асинхронное обращение к внешнему ресурсу будем имитировать с 
            //помощью Task.Delay()
            await Task.Delay(100); // Создаем задачу, которая будет заверешена через 100 миллисекунд
        }
         
        //Tuple<long, int>, в котором сохранены время выполнения теста и зафиксированное число использованных потоков.
        public Tuple<long, int> ExecuteParallelTest()
        {
            this._loggedThreads.Clear();

            var sw = new Stopwatch();
            sw.Restart();

            Parallel.For(0, _maxTasks, this.LongRunningTask);

            sw.Stop();
            return new Tuple<long, int>(sw.ElapsedMilliseconds, this._loggedThreads.Count);
        }

        public Tuple<long, int> ExecuteTaskTest()
        {
            this._loggedThreads.Clear();

            var tasks = new List<Task>();

            var sw = new Stopwatch();
            sw.Restart();

            for (var i = 0; i < _maxTasks; i++)
            {
                var closure = i;
                tasks.Add(Task.Run(() => this.LongRunningTask(closure)));
            }
            Task.WaitAll(tasks.ToArray());

            sw.Stop();

            return new Tuple<long, int>(sw.ElapsedMilliseconds, this._loggedThreads.Count);
        }

        public Tuple<long, int> ExecuteAwaitTest()
        {
            this._loggedThreads.Clear();

            var tasks = new List<Task>();

            var sw = new Stopwatch();
            sw.Restart();
            //Создаем задачи и добавляем их в список задач
            for (var i = 0; i < _maxTasks; i++)
                tasks.Add(this.LongRunningAsync());
            Task.WaitAll(tasks.ToArray());//Запускаем задачи на выполнение

            sw.Stop();

            return new Tuple<long, int>(sw.ElapsedMilliseconds, this._loggedThreads.Count);
        }



        private void LogCurrentThread()
        {
            lock (this._lock)
            {
                var threadId = Thread.CurrentThread.ManagedThreadId;
                //если нового потока еще нет в списке потоков, добавляем его в список потоков
                if (this._loggedThreads.All(tid => tid != threadId))
                    this._loggedThreads.Add(threadId);
            }
        }

        class Program
        {
            private static readonly TestMethods _tests = new TestMethods();

            static void Main(string[] args)
            {
                Program.RunTest("await", _tests.ExecuteAwaitTest);
                Program.RunTest("Parallel.For", _tests.ExecuteParallelTest);
                Program.RunTest("Task.Run", _tests.ExecuteTaskTest);

                Console.WriteLine("\n\nХорошо видно, что в случае с использованием async/await\n время выполнения теста приблизительно равно времени \"ожидания ответа\".\n Кроме того, было зафиксировано использование только одного потока. Схожий\n код (см. методы ExecuteAawitTest() и ExecuteTaskTest()) с использованием\n Task.Run() не сильно отличается по результатам от Parallel.For(). Оба этих\n теста использовали достаточно большое количество потоков, при этом часть\n из них попросту простаивала.");
                Console.ReadKey(true);
            }

            static void RunTest(string testName, Func<Tuple<long, int>> testMethod)
            {
                const int testLoops = 10;

                var elapsedTime = new List<long>();
                var threadCount = new List<int>();

                for (var i = 0; i < testLoops; i++)
                {
                    var result = testMethod();
                    elapsedTime.Add(result.Item1);
                    threadCount.Add(result.Item2);
                }

                Console.WriteLine("{0} average time: {1} ms", testName, elapsedTime.Average());
                Console.WriteLine("Thread count: min {0} – max {1}",
                    threadCount.Min(),
                    threadCount.Max());
            }
        }
    }
}