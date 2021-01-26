/*Метод может быть полностью синхронизирован с помощью атрибута MethodlmplAttribute. 
 Такой подход может стать альтернативой оператору lock в тех случаях, когда метод 
требуется заблокировать полностью. Атрибут
 MethodlmplAttribute определен в пространстве имен System.Runtime.CompilerServices. 
Ниже приведен конструктор, применяемый для подобной синхронизации:
public MethodlmplAttribute(MethodlmplOptions methodlmplOptions)
где methodImplOptions обозначает атрибут реализации. 
Для синхронизации метода достаточно указать атрибут MethodImplOptions.Synchronized. 
Этот атрибут вызывает блокировку всего метода для текущего экземпляра объекта, 
доступного по ссылке this. Если же метод относится к типу static, то блокируется его тип. 
Поэтому данный атрибут непригоден для применения в открытых объектах или классах.
Ниже приведена еще одна версия программы, имитирующей тиканье часов, 
с переделанным вариантом класса TickTock, в котором атрибут MethodlmplOptions обеспечивает 
должную синхронизацию.
 */
// Use MethodImplAttribute to synchronize a method. 

//MethodImplAttribute - класс аттрибут 
//Сообщает подробные сведения о реализации метода
/*
 Атрибуты в .NET представляют специальные инструменты, которые позволяют встраивать в сборку дополнительные метаданные. Атрибуты могут применяться как ко всему типу (классу, интерфейсу и т.д.), так и к отдельным его частям (методу, свойству и т.д.). Основу атрибутов составляет класс System.Attribute, от которого образованы все остальные классы атрибутов.

В .NET имеется множество различных классов атрибутов. 
Например, при сериализации в различные форматы используются атрибуты [Serializable] и [NonSerialized]. 
С помощью рефлексии стандартные классы .NET получают использованные атрибуты и производят определенные 
действия. Например, атрибут [Serializable] указывает классу BinaryFormatter, 
что объекты с данным атрибутом можно сохранять в бинарный файл. 
В то ж время пока к классу с атрибутом не применена рефлексия, 
атрибут не размещается в памяти, и никакого влияния на данный класс не оказывает.
*/
using System;
using System.Threading;
using System.Runtime.CompilerServices;

// Rewrite of TickTock to use MethodImplOptions.Synchronized. 
class TickTock
{

    /* The following attribute synchronizes the entire 
       Tick() method. */
    [MethodImplAttribute(MethodImplOptions.Synchronized)]
    public void Tick(bool running)
    {
        if (!running)
        { // stop the clock 
            Monitor.Pulse(this); // notify any waiting threads 
            return;
        }

        Console.Write("Tick ");
        Monitor.Pulse(this); // let Tock() run 

        Monitor.Wait(this); // wait for Tock() to complete 
    }


    /* The following attribute synchronizes the entire 
       Tock() method. */
    [MethodImplAttribute(MethodImplOptions.Synchronized)]
    public void Tock(bool running)
    {
        if (!running)
        { // stop the clock 
            Monitor.Pulse(this); // notify any waiting threads 
            return;
        }

        Console.WriteLine("Tock");
        Monitor.Pulse(this); // let Tick() run 

        Monitor.Wait(this); // wait for Tick() to complete 
    }
}

class MyThread
{
    public Thread Thrd;
    TickTock ttOb;

    // Construct a new thread. 
    public MyThread(string name, TickTock tt)
    {
        Thrd = new Thread(this.Run);
        ttOb = tt;
        Thrd.Name = name;
        Thrd.Start();
    }

    // Begin execution of new thread. 
    void Run()
    {
        if (Thrd.Name == "Tick")
        {
            for (int i = 0; i < 5; i++) ttOb.Tick(true);
            ttOb.Tick(false);
        }
        else
        {
            for (int i = 0; i < 5; i++) ttOb.Tock(true);
            ttOb.Tock(false);
        }
    }
}

class TickingClock
{
    static void Main()
    {
        TickTock tt = new TickTock();
        MyThread mt1 = new MyThread("Tick", tt);
        MyThread mt2 = new MyThread("Tock", tt);

        mt1.Thrd.Join();
        mt2.Thrd.Join();
        Console.WriteLine("Clock Stopped");
        Console.ReadKey();
    }
}