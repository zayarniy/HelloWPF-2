using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/*
 *  Предупреждение
Никогда не следует вызывать метод Join объекта Thread, 
который представляет текущий поток из текущего потока. 
Это приведет к тому, что ваше приложение перестанет отвечать, 
поскольку текущий поток ждет, пока не исключается.
*/
    class Program
    {
        static void Main(string[] args)
        {
        Console.WriteLine("Специально сделаем, чтобы наш поток повис");
        Console.WriteLine("Так как он будет дожидаться собственного завершения");
        Thread.CurrentThread.Join();
        }
    }

