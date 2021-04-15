// Cancel a parallel query. 

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

class PLINQCancelDemo
{

    static void Main()
    {
        //************************* объект для прекращения запроса ************************
        CancellationTokenSource cancelTokSrc = new CancellationTokenSource();
        //*********************************************************************************
        
        int[] data = new int[20000000];

        // Initialize the data to positve values. 
        for (int i = 0; i < data.Length; i++) data[i] = i;

        // Now, insert some negative values. 
        data[1000] = -1;
        data[14000] = -2;
        data[15000] = -3;
        data[676000] = -4;
        data[8024540] = -5;
        data[9908000] = -6;

        // Use a PLINQ query to find the negative values. 
        var negatives = from val in data.AsParallel().
                                    WithCancellation(cancelTokSrc.Token)
                        where val < 0
                        select val;

        // Create a task that cancels the query after 50 milliseconds. 
        //Возможно время отмены запроса нужно будет уменьшить в зависимости от мощности вашего компьютера
        //Создаем поток, который выжидает какое-то время, а потом сообщает через cancelTokSrc о прекращении выполнения запроса

        Task cancelTsk = Task.Factory.StartNew(() => {
            Thread.Sleep(50);
            cancelTokSrc.Cancel();
        });

        try
        {
            foreach (var v in negatives)
                Console.Write(v + " ");
        }
        catch (OperationCanceledException exc)
        {
            Console.WriteLine("OperationCanceledException");            
            Console.WriteLine(exc.Message);
        }
        catch (AggregateException exc)
        {
            Console.WriteLine("AggregateException");
            Console.WriteLine(exc);
        }
        finally
        { 
            cancelTsk.Wait();
            cancelTokSrc.Dispose();
            cancelTsk.Dispose();
        }

        Console.WriteLine();

        Console.Read();


    }
}
/*
Отмена параллельного запроса
Параллельный запрос отменяется таким же образом, как и задача. И в том и в другом случае отмена опирается на структуру CancellationToken, получаемую из класса CancellationTokenSource. Получаемый в итоге признак отмены передается запросу с помощью метода WithCancellation (). Отмена параллельного запроса производится методом Cancel (), который вызывается для источника признаков отмены. Главное отличие отмены параллельного запроса от отмены задачи состоит в следующем: когда параллельный запрос отменяется, он генерирует исключение OperationCanceledException, а не AggregateException. Но в тех случаях, когда запрос способен сгенерировать несколько исключений, исключение OperationCanceledException может быть объединено в совокупное исключение AggregateException. Поэтому отслеживать лучше оба вида исключений.
*/
/*
В приведенном примере программы демонстрируется порядок отмены параллельного запроса, сформированного в программе из предыдущего примера. В данной программе организуется отдельная задача, которая ожидает в течение 50 миллисекунд, а затем отменяет запрос. Отдельная задача требуется потому, что цикл foreach, в котором выполняется запрос, блокирует выполнение метода Main () до завершения цикла. 
*/
