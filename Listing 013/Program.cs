//Использование ParallelLoolResult, ParallelLoopState and Break с Parallel.For

using System;
using System.Threading.Tasks;

class DemoParallelForWithLoopResult
{
    static int[] data;

    // A method to be run as the body of a parallel loop. 
    // The statements in this loop are designed to simply 
    // consume some CPU time for the puposes of demonstration. 
    static void MyTransform(int i, ParallelLoopState pls)
    {
         
        // Break out of loop if a negative value is found. 
        if (data[i] < 0) pls.Break();

        // So something for demonstration purposes. 
        data[i] = data[i] / 10;

        if (data[i] < 1000) data[i] = 0;
        if (data[i] > 1000 & data[i] < 2000) data[i] = 100;
        if (data[i] > 2000 & data[i] < 3000) data[i] = 200;
        if (data[i] > 3000) data[i] = 300;
    }

    static void Main()
    {

        Console.WriteLine("Main thread starting.");

        data = new int[100000000];

        // Initialize data. 
        for (int i = 0; i < data.Length; i++) data[i] = i;

        // Put a negative value into data. 
        data[1005] = -10;

        // Parallel transform loop. 
        ParallelLoopResult loopResult =
                    Parallel.For(0, data.Length, MyTransform);

        // See if the loop ran to completion. 
        if (!loopResult.IsCompleted)
            Console.WriteLine("\nLoop Terminated early because a " +
                              "negative value was encountered\n" +
                              "in iteration number " +
                               loopResult.LowestBreakIteration + ".\n");

        Console.WriteLine("Main thread ending.");

        Console.Read();

    }
}
/*
Как упоминалось выше, метод For () возвращает экземпляр объекта типа ParallelLoopResult. Его структура, в которой определяются два следующих свойства.

public bool IsCompleted { get; }
public Nullable<long> LowestBreakIteration { get; }

Свойство IsCompleted будет иметь логическое значение true, если выполнены все шаги цикла. Иными словами, при нормальном завершении цикла это свойство будет содержать логическое значение true. Если же выполнение цикла прервется раньше времени, то данное свойство будет содержать логическое значение false. Свойство LowestBreaklteration будет содержать наименьшее значение переменной управления циклом, если цикл прервется раньше времени вызовом метода ParallelLoopState.Break().
Для доступа к объекту типа ParallelLoopState следует использовать форму метода For (), делегат которого принимает в качестве второго параметра текущее состояние цикла.
Ниже эта форма метода For () приведена в простейшем виде.
public static ParallelLoopResult For(int fromInclusive, int toExclusive,ActionCint, ParallelLoopState> body)
В данной форме делегат Action, описывающий тело цикла, определяется следующим образом.
public delegate void Action<in Tl, in T2>(T argl, T2 arg2)
Для метода For () обобщенный параметр Tl должен быть типа int, а обобщенный параметр Т2 — типа ParallelLoopState. Всякий раз, когда делегат Action вызывается, текущее состояние цикла передается в качестве аргумента агд2.
Для преждевременного завершения цикла следует воспользоваться методом Break (), вызываемым для экземпляра объекта типа ParallelLoopState внутри тела цикла, определяемого параметром body. Метод Break () объявляется следующим образом.
public void Break()
*/
