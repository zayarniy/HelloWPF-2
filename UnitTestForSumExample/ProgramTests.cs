using Microsoft.VisualStudio.TestTools.UnitTesting;
using SumExampleForUnitTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumExampleForUnitTest.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void SumTest()
        {
            //В примере показан пример использования Assert.Fail.
            //Для успешного прохождения теста, нужно чтобы вызвалось исключение
            //без этого тест завален          
            try
            {
                //new MyClass().DoSomething(BAD_INPUT);
                SumExampleForUnitTest.Program.SumString(new string('*', int.MaxValue), new string('*', int.MaxValue));//Вызов приводящий к исключению
                Assert.Fail("No exception was thrown");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                //Assert.AreEqual(BAD_INPUT, ex.Message);
            }
        }

        [TestMethod()]
        public void SumTest2()
        {
            //В примере показан пример использования Assert.Fail.
            //Для успешного прохождения теста, нужно чтобы вызвалось исключение
            //без этого тест завален          
            try
            {
                //new MyClass().DoSomething(BAD_INPUT);
                Assert.ThrowsException<Exception>(SumExampleForUnitTest.Program.SumString(new string('*', int.MaxValue), new string('*', int.MaxValue));//Вызов приводящий к исключению
                Assert.Fail("No exception was thrown");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                //Assert.AreEqual(BAD_INPUT, ex.Message);
            }
        }

    }
}