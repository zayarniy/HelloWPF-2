/*
Вместо опроса делегата с целью определения, завершился ли асинхронно вызванный метод, 
было бы более эффективно заставить вторичный поток информировать вызывающий поток о завершении 
выполнения задачи. Чтобы сделать такое поведение возможным, понадобится передать методу Beginlnvoke() 
в качестве параметра экземпляр делегата System.AsyncCallback; до сих пор для этого параметра 
указывалось значение null. Тем не менее, когда предоставляется объект AsyncCallback, 
делегат будет автоматически вызывать указанный метод после завершения асинхронного вызова.
На заметку! Метод обратного вызова будет вызываться во вторичном потоке, 
а не в первичном. Данный факт имеет важные последствия для потоков внутри 
графического пользовательского интерфейса (WPF или Windows Forms), 
т.к. элементы управления привязаны к потоку, в котором они были созданы, и могут обрабатываться 
только в нем. При рассмотрении библиотеки TPL и новых ключевых слов async и await языка C# 
далее в главе будут представлены некоторые примеры работы потоков из графического пользовательского 
интерфейса.
*/
using System;
using System.Threading;
using System.Runtime.Remoting.Messaging;

//TextTokenizerWPF - еще один пример на использование CallbackDelegate
namespace AsyncCallbackDelegate
{
    public delegate int BinaryOp(int x, int y);

    class Program
    { 
        //Флаг окончания
        private static bool isDone = false;
        /*
        Строго говоря, использование булевской переменной в данном примере не является безопасным 
        в отношении потоков, поскольку к ее значению имеют доступ два разных потока. 
        В текущем примере подобное допустимо; тем не менее, запомните в качестве очень важного 
        эмпирического правила: вы должны обеспечивать блокировку данных, 
        разделяемых между несколькими потоками. Вы увидите, как это делается, далее.        
        */
        static void Main(string[] args)
        {
            Console.WriteLine("*****  AsyncCallbackDelegate Example *****");
            Console.WriteLine("Main() invoked on thread {0}.",
              Thread.CurrentThread.ManagedThreadId);

            BinaryOp b = new BinaryOp(Add);
            IAsyncResult ar = b.BeginInvoke(10, 10,
              new AsyncCallback(AddComplete),//Используем делегат AsyncCallback, чтобы разметить в нем метод обратного вызова
              "Main() thanks you for adding these numbers.");

            // Assume other work is performed here...
            while (!isDone)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Working....");
            }

            Console.ReadLine();
        }

        #region Target for AsyncCallback delegate
        // Don't forget to add a 'using' directive for 
        // System.Runtime.Remoting.Messaging!
        static void AddComplete(IAsyncResult iar)
        {
            Console.WriteLine("AddComplete() invoked on thread {0}.",
              Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Your addition is complete");

            // Now get the result.
            AsyncResult ar = (AsyncResult)iar;
            BinaryOp b = (BinaryOp)ar.AsyncDelegate;
            Console.WriteLine("10 + 10 is {0}.", b.EndInvoke(iar));

            // Retrieve the informational object and cast it to string.
            string msg = (string)iar.AsyncState;
            Console.WriteLine(msg);

            isDone = true;
        }

        #endregion

        #region Target for BinaryOp delegate
        static int Add(int x, int y)
        {
            Console.WriteLine("Add() invoked on thread {0}.",
              Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(5000);
            return x + y;
        }
        #endregion
    }
}

