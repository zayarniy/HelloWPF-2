using System;
//based on https://www.bestprog.net/ru/2018/08/27/example-of-creating-a-unit-test-in-ms-visual-studio-2010-c_ru/
/*
В данной работе в решении(Solution) сформированы два проекта.Один проект SumExampleForUnitTest содержит функцию SumExampleForUnitTest.Program.Sum(), которую нужно протестировать.Второй проект UnitTestForSumExample содержит тестирующие методы.

В Microsoft Visual Studio 2010 каждый из проектов запускается с помощью разных команд меню.Так, проект MinApp запускается стандартным способом из меню Run. А проект TestMinApp запускается из специального меню Test.
*/


//Это проект для тестирования
namespace SumExampleForUnitTest
{
    public class Program 
    {
        public static int Sum(int a, int b)
        {
            return a + b;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Демонстрация Unit-тестов в C#.");
        }
    }
}
