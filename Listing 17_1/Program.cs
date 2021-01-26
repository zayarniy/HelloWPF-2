// Демонстрация управления основным потоком.
using System;
using System.Threading;
class UseMain
{
    static void Main()
    {
        Thread Thrd;
        // Получить основной поток.
        Thrd = Thread.CurrentThread;
        // Отобразить имя основного потока.
        if (Thrd.Name == null)
            Console.WriteLine("У основного потока нет имени.");
        else
            Console.WriteLine("Основной поток называется: " + Thrd.Name);
    // Отобразить приоритет основного потока.
        Console.WriteLine("Приоритет: " + Thrd.Priority);
        Console.WriteLine();
        // Установить имя и приоритет.
        Console.WriteLine("Установка имени и приоритета.\n");
        Thrd.Name = "Основной Поток";
        Thrd.Priority = ThreadPriority.AboveNormal;
        Console.WriteLine("Теперь основной поток называется: " +
        Thrd.Name);
        Console.WriteLine("Теперь приоритет: " + Thrd.Priority);

        Console.ReadKey();

    }
}