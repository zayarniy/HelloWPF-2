using System;
using Ivanov;

/* 
Для демонстрации этого примера, нужно 
разместить библиотеку Ivanov в GAC, 
потом подключить библиотеку Ivanov и в свойствах убрать переключатель Копировать локально
Запустить проект и показать, что Ivanov.dll не скопировался в папку с проектом, а компилятор ссылается на него
в GAC

Для демонстрации можно удалить Ivanov.dll из GAC
gacutil.exe /u "J:\C#-3\HelloWPF-2\Ivanov\bin\Debug\Ivanov.dll"
 
и продемонстрировать, что проект не запустится
*/

namespace GAC_Model_Usage
{
    class Program
    {
        static Matrix A,B;        
        
        static void Main(string[] args)
        {
            A = new Matrix(50, 50);
            B = new Matrix(50, 50);
            Console.WriteLine("Start");
            A.MultiplyOverThread(B);
            Console.WriteLine("Done");
            Console.ReadKey();
        }
    }
}
