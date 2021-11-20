//Пример программы, которая может! зависнуть без использования модификатора volatile
using System.Threading;
using System.Threading.Tasks;
 
namespace VolatileExample
{
    class ReorderTest
    {
        private int _a;//может зависнуть (как правило не зависает)
        //private volitale int _a;//не может зависнуть
        public void Foo()
        {
            var task = new Task(Bar);
            task.Start();
            Thread.Sleep(1000);
            _a = 0;
            task.Wait();
        }

        public void Bar()
        {
            _a = 1;
            while (_a == 1)
            {
            }
        }
    }

    class Program
    {
        static void Main()
        {
            ReorderTest reorderTest = new ReorderTest();
            reorderTest.Foo();
        }
    }


    
}
/*/
 * 
https://habr.com/ru/post/130318/
Запустив этот пример можно убедится, что программа зависает. Причина кроется в том, что компилятор кэширует переменную _a в регистре процессора.
Для решения подобных проблем C# предоставляет ключевое слово volatile. Применение этого ключевого слова к переменной запрещает компилятору как-либо оптимизировать обращения к ней.
*/