using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankAccountNS;
//Цель - продемострировать некоторые возможности Unit тестов. Нужно исправить ошибки в процедурах тестирования

namespace BankTest
{

    //Атрибут [TestClass] является обязательным в любом классе, содержащем методы модульных тестов, которые необходимо выполнить в обозревателе тестов.

    /*
    Можно иметь другие классы в проекте модульного теста, которые не содержат атрибута [TestClass] , а также иметь другие методы в тестовых классах, у которых атрибут — [TestMethod] . Можно вызывать эти другие классы и методы в методах теста.
    */
    [TestClass]
    public class BankAccountTests
    {
        //Каждый метод теста, предназначенный для запуска в обозревателе 
        //тестов, должен иметь атрибут [TestMethod].
        //[TestMethod]
        //public void TestMethod1()
        //{
        //}



        /*
        В этой процедуре мы напишем методы модульного теста для проверки поведения метода Debit класса BankAccount.
    Существует по крайней мере три поведения, которые требуется проверить:
        1. Метод создает исключение ArgumentOutOfRangeException, если сумма по дебету превышает баланс.
        2. Метод создает исключение ArgumentOutOfRangeException, если сумма по дебету меньше нуля.
        3. Если значение дебета допустимо, то метод вычитает сумму дебета из баланса счета.
        */


        #region  Test 1. Debit test
        /// <summary>
        /// Первый тест проверяет, снимается ли со счета нужная сумма при допустимом размере кредита (со значением меньшим, чем баланс счета, и большим, чем ноль).
        /// </summary>
        [TestMethod]
        public void Test1_Debit_WithValidAmount_UpdatesBalance()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = 4.55;
            double expected =7.44;// - исправить
            BankAccount account = new BankAccount("Иван Иванов", beginningBalance);

            // Act
            account.Debit1(debitAmount);

            // Assert
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.01, "Снятие с аккаунта не корректно");
        }
        /*
        Метод очень прост: он создает новый объект BankAccount с начальным балансом, а затем снимает допустимое значение. 
        Он использует метод Assert.AreEqual, чтобы проверить, что конечный баланс соответствует ожидаемому.         
        */

        /*
            Требования к методу теста
            Метод теста должен удовлетворять следующим требованиям:
            Он декорируется атрибутом [TestMethod].
            Он возвращает void.
            Он не должен иметь параметров.         
        */
        #endregion

        #region Test 2. Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange1
        //Мы используем метод ThrowsException для подтверждения правильности созданного исключения. 
        //Этот метод приводит к тому, что тест не будет пройден, если не возникнет исключения
        //ArgumentOutOfRangeException.
        [TestMethod]
        public void Test2_Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange1()
        {
            // Arrange
            double beginningBalance = -11.99;
            double debitAmount = 100.00;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);
            // Act and assert
            //Ожидается исключение. Оно происходит и это правильно. Исправить заменой на  ArgumentOutOfRangeException
            Assert.ThrowsException<System.ArgumentOutOfRangeException>(() => account.Debit2(debitAmount));
        }
        /*
          Если временно изменить тестируемый метод для вызова более общего исключения ApplicationException при значении суммы по дебету меньше нуля, 
        то тест работает правильно — то есть завершается неудачно.
        Чтобы проверить случай, когда размер списания превышает баланс, выполните следующие действия:
        Создать новый метод теста с именем Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange.
        Скопировать тело метода из Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange в новый метод.
        Присвоить debitAmount значение, превышающее баланс.
        Выполните два теста и убедитесь, что они пройдены.* 
         */
        #endregion


        #region Test 3. Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange2()
        /*
Тестируемый метод можно дополнительно улучшить. При такой реализации мы не можем знать, какое условие (amount > m_balance или amount < 0) приводят к исключению, возвращаемому в ходе теста. Нам просто известно, что ArgumentOutOfRangeException где-то возникает в методе. Было бы лучше знать, какое условие в BankAccount.Debit вызвало исключение (amount > m_balance или amount < 0), чтобы быть уверенными в том, что наш метод правильно проверяет свои аргументы.
Еще раз проанализировав тестируемый метод BankAccount.Debit, можно заметить, что оба условных оператора используют конструктор ArgumentOutOfRangeException, 
        который просто получает имя аргумента в качестве параметра:   
        **************************************************
        *throw new ArgumentOutOfRangeException("amount");*
        **************************************************
        */

        /*
Так выглядит конструктор, который можно использовать для сообщения более детальной информации: 
        *****************************************************
        *ArgumentOutOfRangeException(String, Object, String)* 
        *****************************************************
        
        включает имя аргумента, значения аргумента и определяемое пользователем сообщение. 
        Мы можем выполнить рефакторинг тестируемого метода для использования данного конструктора. 
        Более того, можно использовать открытые для общего доступа члены типа для указания ошибок.         
        */
        //ТЕСТ ПРОХОДИТ КАК ПРАВИЛЬНЫЙ И ЭТО И ЕСТЬ ОШИБКА. Нет извещения об ошибке. Исправлено в Test4
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
        Выполните рефакторинг методов теста, удалив вызов Assert.ThrowsException. Заключите вызов Debit() в блок try/catch,
        перехватите конкретное ожидаемое исключение и проверьте соответствующее ему сообщение. 
        Метод Microsoft.VisualStudio.TestTools.UnitTesting.StringAssert.Contains обеспечивает возможность сравнения двух строк. 
        */
        #endregion

        #region Test 4. Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()

        /*
Метод теста сейчас обрабатывает не все требуемые случаи. Если тестируемый метод Debit не смог выдать исключение 
//ArgumentOutOfRangeException, когда значение debitAmount было больше остатка (или меньше нуля), 
//метод теста выдает успешное прохождение. Это нехорошо, поскольку метод теста должен был 
завершиться с ошибкой в том случае, если исключение не создается.
Это является ошибкой в методе теста. Для решения этой проблемы добавим утверждение Fail в конце 
тестового метода для обработки случая, когда исключение не создается.
Однако повторный запуск теста показывает, что тест теперь оказывается непройденным при перехватывании верного исключения. 
Блок catch перехватывает исключение, но метод продолжает выполняться, и в нем происходит сбой на новом утверждении Fail. 
Чтобы разрешить эту проблему, добавим оператор return после StringAssert в блоке catch. Повторный запуск теста подтверждает,
что проблема устранена. Окончательная версия метода Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange выглядит 
        следующим образом:          
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

            Assert.Fail("Ожидался вызов исключения о том, что снятие больше чем баланс, но этого не произошло.");
        }
        #endregion
    }
}

