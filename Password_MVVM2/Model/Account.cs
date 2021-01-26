using System;
using System.Collections.Generic;

//******** БИЗНЕС ЛОГИКА ПРОЕКТА *****************

namespace Password_MVVM.Model
{
   //Класс для логина и пароля
   public class Account
    {
        public string Login { get; set; } = "None";
        public string Password { get; set; } = "None";

        public Account(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }

    //класс для аккаунтов
    class Accounts
    {


        public bool Checks(Account account)
        {
            foreach (Account acc in ListAccounts)
                if (acc.Login==account.Login && acc.Password==account.Password) return true;
            return false;
        }

        //Класс получающий аккаунты - заменить на получение аккаунтов из базы данных (см. пример получения списков емейлов)
        public static IEnumerable<Account> ListAccounts
        {
            get;
        }
            = new List<Account>() {
                                  new Account("root", "root"),
                                  new Account("login","password"),
                                  new Account("admin","admin")
                                  };
    }
}
