﻿using System;
using System.Threading;

/*
Наряду с оператором lock для синхронизации потоков мы можем использовать мониторы, 
представленные классом System.Threading.Monitor. Фактически конструкция оператора lock 
из прошлой темы инкапсулирует в себе синтаксис использования мониторов. 
А рассмотренный в прошлой теме пример будет эквивалентен следующему коду: 
*/
namespace Listing_11_Monitors
{
    class Program
    {
        static int x = 0;
        static object locker = new object();
        static void Main(string[] args)
        {
            for (int i = 0; i < 5; i++)
            {
                Thread myThread = new Thread(Count);
                myThread.Name = $"Поток {i.ToString()}";
                myThread.Start();
            }

            Console.ReadLine();
        }
        public static void Count()
        {
            bool acquiredLock = false;
            try
            {
                Monitor.Enter(locker, ref acquiredLock);
                x = 1;
                for (int i = 1; i < 9; i++)
                {
                    Console.WriteLine($"{Thread.CurrentThread.Name}: {x}");
                    x++;
                    Thread.Sleep(100);
                }
            }
            finally
            {
                if (acquiredLock) Monitor.Exit(locker);
            }
        }
    }
}
/*
Метод Monitor.Enter принимает два параметра - объект блокировки и значение типа bool, 
которое указывает на результат блокировки (если он равен true, то блокировка успешно 
выполнена). Фактически этот метод блокирует объект locker так же, 
как это делает оператор lock. С помощью А в блоке try...finally с помощью метода 
Monitor.Exit происходит освобождение объекта locker, если блокировка осуществлена успешно,
и он становится доступным для других потоков.

Кроме блокировки и разблокировки объекта класс Monitor имеет еще ряд методов, 
которые позволяют управлять синхронизацией потоков. 
Так, метод Monitor.Wait освобождает блокировку объекта и переводит поток в 
очередь ожидания объекта. Следующий поток в очереди готовности объекта 
блокирует данный объект. А все потоки, которые вызвали метод Wait, остаются в очереди 
ожидания, пока не получат сигнала от метода Monitor.Pulse или Monitor.PulseAll, 
посланного владельцем блокировки. Если метод Monitor.Pulse отправил сигнал, 
то поток, находящийся во главе очереди ожидания, получает сигнал и блокирует освободившийся 
объект. Если же метод Monitor.PulseAll отправлен, то все потоки, находящиеся в очереди 
ожидания, получают сигнал и переходят в очередь готовности, 
где им снова разрешается получать блокировку объекта.
*/