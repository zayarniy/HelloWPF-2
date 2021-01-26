using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FibonacciTDD
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void testFibonacci0()
        {
            Assert.AreEqual(0, fib0(0));//Первый тест
        }

        //В проекте для учеников все делать в методе fib
        //Лучше его вынести в отдельный проект
        //Первая версия
        int fib0(int n)
        {
            return 0;
        }

        [TestMethod]
        public void testFibonacci1()
        {
            Assert.AreEqual(0, fib0(0));//Второй тест для fib1
            Assert.AreEqual(1, fib1(1));
        }

        //Вторая версия
        int fib1(int n)
        {
            if (n == 0) return 0;
            else return 1;
        }

         
        [TestMethod]
        public void testFibonacci2()//убираем дублирование
        {
            //Описываем словарь, где ключ - это N, а значение - это результат
            Dictionary<int,int> cases =new Dictionary<int, int>() 
            { { 0, 0 }, { 1, 1 } };
            foreach(var @case in cases)
                Assert.AreEqual(@case.Value, fib2(@case.Key));
        }

        //Вторая версия
        int fib2(int n)
        {
            if (n == 0) return 0;
            else return 1;
        }

        [TestMethod]
        public void testFibonacci3()//теперь достаточно добавить значение в словарь
        {
            //Описываем словарь, где ключ - это N, а значение - это результат
            Dictionary<int, int> cases = new Dictionary<int, int>()
            { { 0, 0 }, { 1, 1 },{2,1 } };
            foreach (var @case in cases)
                Assert.AreEqual(@case.Value, fib3(@case.Key));
        }

        int fib3(int n)
        {
            if (n == 0) return 0;
            else return 1;
        }

        [TestMethod]
        public void testFibonacci4()//ошибка!
        {
            //Описываем словарь, где ключ - это N, а значение - это результат
            Dictionary<int, int> cases = new Dictionary<int, int>()
            { { 0, 0 }, { 1, 1 },{2,1 },{3,2 } };
            foreach (var @case in cases)
                Assert.AreEqual(@case.Value, fib4(@case.Key));
        }

        //Вторая версия
        int fib4(int n)
        {
            if (n == 0) return 0;
            if (n == 1) return 1;//4.теперь окончательно чистим код, та же самая структура должна работать и для fib(2),поэтому преобразовываем второй условный оператор
            //return 1+1;//1. мы написал два, но на самом деле имели в виду 1+1
            //return fib4(n - 1) + 1;//2. Но первая 1 на самом деле fib(n-1)+1
            return fib4(n - 1) + fib4(n-2);//3. А вторая единица в сумме - на самом деле fib(n-2)
        }
        //5. Это и есть функция вычисления последовательности Фибоначчи целиком и полностью разработанная в рамках методики TDD

    }
}
