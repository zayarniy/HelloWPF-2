using System;
using System.Collections.Generic;
using System.ComponentModel;

//******** БИЗНЕС ЛОГИКА ПРОЕКТА *****************

namespace Password_MVVM.Model
{
    //Класс для логина и пароля
    public class Account : INotifyPropertyChanged
    {
        private string login = "None";
        private string password = "None";

        public string Login
        {
            get => login; set
            {
                if (value != login)
                {
                    login = value;
                    //Если у элемента OneWayToSource - PropertyChanged.Invoke не обновляет данные             
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Login"));
                }
            }
        }
        public string Password { get => password; set => password = value; }
        public Account(string login, string password)
        {
            this.login = login;
            this.password = password;
        }

        public event PropertyChangedEventHandler PropertyChanged;
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
