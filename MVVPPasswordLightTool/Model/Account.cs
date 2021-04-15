using System;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;

//******** БИЗНЕС ЛОГИКА ПРОЕКТА *****************

namespace MVVMPasswordLightTool.Model
{
    //Класс для логина и пароля
    public class Account : INotifyPropertyChanged
    {
        private string login = "None";
        private string password = "None";

        public event PropertyChangedEventHandler PropertyChanged;

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
            //Лучше обращаться 
            this.login = login;
            Password = password;
        }
    }

    //класс для аккаунтов
    class Accounts//:ObservableObject
    {


        //Проверка аккаунта
       static public bool Checks(Account account)
        {
            foreach (Account acc in ListAccounts)
                if (acc.Login==account.Login && acc.Password==account.Password) return true;
            return false;
        }

        //Класс получающий аккаунты - заменить на получение аккаунтов из базы данных (см. пример получения списков емейлов)
        public static ObservableCollection<Account> ListAccounts
        {
            get;
        }
            = new ObservableCollection<Account>() {
                                  new Account("root", "root"),
                                  new Account("login","password"),
                                  new Account("admin","admin")
                                  };
    }
}
