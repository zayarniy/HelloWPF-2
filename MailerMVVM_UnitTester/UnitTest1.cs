using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MailerMVVM_UnitTester
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCode()
        {
            Mailer.Model.Password password = new Mailer.Model.Password();

            string code=password.Code("abc", 1);
            Assert.AreEqual(code, "bcd");
        }

        [TestMethod]
        public void TestDecode()
        {
            Mailer.Model.Password password = new Mailer.Model.Password();

            string decode = password.Decode("bcd", 1);
            Assert.AreEqual(decode, "abc");
        }


    }
}
