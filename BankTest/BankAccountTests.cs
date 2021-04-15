using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankAccountNS;
//���� - ����������������� ��������� ����������� Unit ������. ����� ��������� ������ � ���������� ������������

namespace BankTest
{

    //������� [TestClass] �������� ������������ � ����� ������, ���������� ������ ��������� ������, ������� ���������� ��������� � ������������ ������.

    /*
    ����� ����� ������ ������ � ������� ���������� �����, ������� �� �������� �������� [TestClass] , � ����� ����� ������ ������ � �������� �������, � ������� ������� � [TestMethod] . ����� �������� ��� ������ ������ � ������ � ������� �����.
    */
    [TestClass]
    public class BankAccountTests
    {
        //������ ����� �����, ��������������� ��� ������� � ������������ 
        //������, ������ ����� ������� [TestMethod].
        //[TestMethod]
        //public void TestMethod1()
        //{
        //}



        /*
        � ���� ��������� �� ������� ������ ���������� ����� ��� �������� ��������� ������ Debit ������ BankAccount.
    ���������� �� ������� ���� ��� ���������, ������� ��������� ���������:
        1. ����� ������� ���������� ArgumentOutOfRangeException, ���� ����� �� ������ ��������� ������.
        2. ����� ������� ���������� ArgumentOutOfRangeException, ���� ����� �� ������ ������ ����.
        3. ���� �������� ������ ���������, �� ����� �������� ����� ������ �� ������� �����.
        */


        #region  Test 1. Debit test
        /// <summary>
        /// ������ ���� ���������, ��������� �� �� ����� ������ ����� ��� ���������� ������� ������� (�� ��������� �������, ��� ������ �����, � �������, ��� ����).
        /// </summary>
        [TestMethod]
        public void Test1_Debit_WithValidAmount_UpdatesBalance()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = 4.55;
            double expected =7.44;// - ���������
            BankAccount account = new BankAccount("���� ������", beginningBalance);

            // Act
            account.Debit1(debitAmount);

            // Assert
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.01, "������ � �������� �� ���������");
        }
        /*
        ����� ����� �����: �� ������� ����� ������ BankAccount � ��������� ��������, � ����� ������� ���������� ��������. 
        �� ���������� ����� Assert.AreEqual, ����� ���������, ��� �������� ������ ������������� ����������.         
        */

        /*
            ���������� � ������ �����
            ����� ����� ������ ������������� ��������� �����������:
            �� ������������ ��������� [TestMethod].
            �� ���������� void.
            �� �� ������ ����� ����������.         
        */
        #endregion

        #region Test 2. Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange1
        //�� ���������� ����� ThrowsException ��� ������������� ������������ ���������� ����������. 
        //���� ����� �������� � ����, ��� ���� �� ����� �������, ���� �� ��������� ����������
        //ArgumentOutOfRangeException.
        [TestMethod]
        public void Test2_Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange1()
        {
            // Arrange
            double beginningBalance = -11.99;
            double debitAmount = 100.00;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);
            // Act and assert
            //��������� ����������. ��� ���������� � ��� ���������. ��������� ������� ��  ArgumentOutOfRangeException
            Assert.ThrowsException<System.ArgumentOutOfRangeException>(() => account.Debit2(debitAmount));
        }
        /*
          ���� �������� �������� ����������� ����� ��� ������ ����� ������ ���������� ApplicationException ��� �������� ����� �� ������ ������ ����, 
        �� ���� �������� ��������� � �� ���� ����������� ��������.
        ����� ��������� ������, ����� ������ �������� ��������� ������, ��������� ��������� ��������:
        ������� ����� ����� ����� � ������ Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange.
        ����������� ���� ������ �� Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange � ����� �����.
        ��������� debitAmount ��������, ����������� ������.
        ��������� ��� ����� � ���������, ��� ��� ��������.* 
         */
        #endregion


        #region Test 3. Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange2()
        /*
����������� ����� ����� ������������� ��������. ��� ����� ���������� �� �� ����� �����, ����� ������� (amount > m_balance ��� amount < 0) �������� � ����������, ������������� � ���� �����. ��� ������ ��������, ��� ArgumentOutOfRangeException ���-�� ��������� � ������. ���� �� ����� �����, ����� ������� � BankAccount.Debit ������� ���������� (amount > m_balance ��� amount < 0), ����� ���� ���������� � ���, ��� ��� ����� ��������� ��������� ���� ���������.
��� ��� ��������������� ����������� ����� BankAccount.Debit, ����� ��������, ��� ��� �������� ��������� ���������� ����������� ArgumentOutOfRangeException, 
        ������� ������ �������� ��� ��������� � �������� ���������:   
        **************************************************
        *throw new ArgumentOutOfRangeException("amount");*
        **************************************************
        */

        /*
��� �������� �����������, ������� ����� ������������ ��� ��������� ����� ��������� ����������: 
        *****************************************************
        *ArgumentOutOfRangeException(String, Object, String)* 
        *****************************************************
        
        �������� ��� ���������, �������� ��������� � ������������ ������������� ���������. 
        �� ����� ��������� ����������� ������������ ������ ��� ������������� ������� ������������. 
        ����� ����, ����� ������������ �������� ��� ������ ������� ����� ���� ��� �������� ������.         
        */
        //���� �������� ��� ���������� � ��� � ���� ������. ��� ��������� �� ������. ���������� � Test4
        [TestMethod]
        public void Test3_Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange2()
        {
            // Arrange 
            double beginningBalance = -11.99;
            double debitAmount = 20.0;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // Act
            try
            {
                account.Debit3(debitAmount);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                // Assert
                Microsoft.VisualStudio.TestTools.UnitTesting.StringAssert.Contains(e.Message, BankAccount.DebitAmountExceedsBalanceMessage);
            }
        }
        /*
        ��������� ����������� ������� �����, ������ ����� Assert.ThrowsException. ��������� ����� Debit() � ���� try/catch,
        ����������� ���������� ��������� ���������� � ��������� ��������������� ��� ���������. 
        ����� Microsoft.VisualStudio.TestTools.UnitTesting.StringAssert.Contains ������������ ����������� ��������� ���� �����. 
        */
        #endregion

        #region Test 4. Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()

        /*
����� ����� ������ ������������ �� ��� ��������� ������. ���� ����������� ����� Debit �� ���� ������ ���������� 
//ArgumentOutOfRangeException, ����� �������� debitAmount ���� ������ ������� (��� ������ ����), 
//����� ����� ������ �������� �����������. ��� ��������, ��������� ����� ����� ������ ��� 
����������� � ������� � ��� ������, ���� ���������� �� ���������.
��� �������� ������� � ������ �����. ��� ������� ���� �������� ������� ����������� Fail � ����� 
��������� ������ ��� ��������� ������, ����� ���������� �� ���������.
������ ��������� ������ ����� ����������, ��� ���� ������ ����������� ������������ ��� �������������� ������� ����������. 
���� catch ������������� ����������, �� ����� ���������� �����������, � � ��� ���������� ���� �� ����� ����������� Fail. 
����� ��������� ��� ��������, ������� �������� return ����� StringAssert � ����� catch. ��������� ������ ����� ������������,
��� �������� ���������. ������������� ������ ������ Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange �������� 
        ��������� �������:          
        */
        [TestMethod]
        public void Test4_Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange3()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = 100.0;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // Act
            try
            {
                account.Debit4(debitAmount);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                // Assert
                StringAssert.Contains(e.Message, BankAccount.DebitAmountExceedsBalanceMessage);
                //StringAssert.Contains(e.Message, BankAccount.DebitAmountLessThanZeroMessage);
                return;
            }

            Assert.Fail("�������� ����� ���������� � ���, ��� ������ ������ ��� ������, �� ����� �� ���������.");
        }
        #endregion
    }
}

