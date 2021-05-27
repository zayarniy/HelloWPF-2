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


        #region Sum for int - The Simplest example
        [TestMethod]
        public void Test1()
        {
            int sum;
            sum = Program.Sum(2, 2);
            Assert.AreEqual(4, sum);
            //Класс Assert содержит несколько статических методов, которые можно использовать для проверки результатов в тестовых методах.
        }

        //Тест с ошибкой
        [TestMethod]
        public void Test2()
        {
            int sum;
            sum = Program.Sum(0, 0);
            Assert.AreEqual(0, sum);
        }
        #endregion

        #region Sum for double

        [TestMethod]
        public void Test3()
        {
            double sum,delta=0.0001; //delta - погрешность вычислений
            sum = Program.SumDouble(1/3, 1/3);
            Assert.AreEqual(2/6, sum, delta);
        }


        [TestMethod]
        public void Test4()
        {
            string actual; //delta - погрешность вычислений
            string expected = "12";
            actual = Program.SumString("1","2");
            Assert.AreEqual(expected, actual);
        }

        static int TestsCount = 0;

        [TestInitialize]
        public void InitTest()
        {
            //будет запускаться перед каждым тестом
            System.IO.File.AppendAllText("tests.log", $"Test {++TestsCount} запушен в {DateTime.Now}\r\n");
        }


        [ClassInitialize]
        public static void InitClass(TestContext context)
        {
            //будет запускаться перед каждым тестом
            System.IO.File.AppendAllText("tests.log", $"Test starts in {DateTime.Now}\r\n");
        }

        #endregion
    }
}
