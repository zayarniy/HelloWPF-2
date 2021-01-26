using System;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Diagnostics;

//******** БИЗНЕС ЛОГИКА ПРОЕКТА *****************

namespace MVVMPasswordLightTool.Model
{
   //Класс для логина и пароля с проверкой на ошибки
   public class Account:IDataErrorInfo
    {
        public string Login { get; set; } 
        public string Password { get; set; }

        public int Number { get; set; }

        //Implement IDataErrorInfo
# region Implement IDataErrorInfo
        public string Error => "";

        public string this[string propertyName]
        {
            get
            {
                string error = "";
                if (propertyName == "Login")
                {
                    if (Login == "" || Login.Length < 2 || Login.Length > 10)
                    {
                        error = "Не правильный логин";
                        Debug.WriteLine(error);
                        return error;
                        //Console.WriteLine(error);                        
                    }
                }
                if (propertyName == "Password")
                {
                    if (Password == "" || Password.Length > 100 || Password.Length < 6)
                    {
                        error = "Необходимо указать пароль";
                        Debug.WriteLine(error);
                        return error;
                        //throw new Exception("Необходимо указать пароль");
                    }
                }

                return "";
            }

        }
        #endregion

        public Account(string login, string password)
        {
            Login = login;
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
