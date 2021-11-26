using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//В этом примере мы запускает запрос без использование AsParallel и с использованием.
//Важно, что AsParallel не всегда запускается в паралельном режиме. 
//Реализация команды сама принимает решение, когда какой способ использовать
namespace PLINQ010
{
    class Program {
        static void Main(string[] args)
        {
            Console.WriteLine("Start any key to start processing as not parallel");
            //Console.ReadKey();
            Console.WriteLine("Processing");
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            ProcessIntData();
            stopwatch.Stop();

            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            Console.WriteLine("Start any key to start processing as parallel");
            //Console.ReadKey();
            Console.WriteLine("Processing");
            stopwatch.Reset();
            stopwatch.Start();
            ProcessIntDataAsParallel();
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            Console.ReadLine();
        }
        static void ProcessIntData()
        {
            // Получить очень большой массив целых чисел.
            int[] source = Enumerable.Range(1, 200_000_000).ToArray();
            // Найти числа, для которых истинно условие num % 3 == 0, 
            // и возвратить их в убывающем порядке.
            int[] modThreeIsZero = (from num in source where num % 3 == 0 orderby num descending select num).ToArray();
            // Вывести количество найденных чисел.
            Console.WriteLine($"Found { modThreeIsZero.Count()} numbers that match query!");
        }

        static void ProcessIntDataAsParallel()
        {
            // Получить очень большой массив целых чисел.
            int[] source = Enumerable.Range(1, 200_000_000).ToArray();
            // Найти числа, для которых истинно условие num % 3 == 0, 
            // и возвратить их в убывающем порядке.
            int[] modThreeIsZero = (from num in source where num % 3 == 0 orderby num descending select num).AsParallel().ToArray();
            // Вывести количество найденных чисел.
            Console.WriteLine($"Found { modThreeIsZero.Count()} numbers that match query!");
        }
    }
}
