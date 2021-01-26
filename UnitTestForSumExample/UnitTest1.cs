using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SumExampleForUnitTest;
//Это проект с помощью которого происходит тестирование
namespace UnitTestForSumExample
{


    [TestClass]
    public class UnitTest1
    {

        //В классе можно вписывать любое количество методов, которые будут тестировать различные функции из разных модулей. 
        //Главное, чтобы эти методы были помечены атрибутом [TestMethod].
        //[TestMethod]
        //public void TestMethod1()
        //{
        //}

        [TestMethod]
        public void Test1()
        {
            int sum;
            sum = Program.Sum(2, 2);
            Assert.AreEqual(4, sum);
        }
         
        //Тест с ошибкой
        [TestMethod]
        public void Test2()
        {
            int sum;
            sum = Program.Sum(0, 0);
            Assert.AreEqual(0, sum);
        }


    }
}
