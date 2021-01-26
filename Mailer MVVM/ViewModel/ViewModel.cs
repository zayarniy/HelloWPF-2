using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mailer.ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {

        //реализация интерфейса INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        //внутрение поля класса
        bool access;
        int attemptCount = 0;

        static public bool GetAccess { get; private set; } = false;

        #region Публичное свойство для привязки "кол-во попыток"
        public int AttemptCount
        {
            get
            { return attemptCount; }

            private set
            {
                if (attemptCount != value)
                {

                    attemptCount = value;
                    //Уведомление о том, что свойство изменилось(для обновления пользовательского интерфейса)
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("AttemptCount"));
                }
            }
        }
        #endregion
        
        Model.EMailSendServiceClass EmailSendServiceClass { get; set; } = new Model.EMailSendServiceClass();

        public string Body { get; set; }

        public List<string> FromList { get; set; } = new List<string>() { "zaazaa@yandex.ru", "putin@kremlin.ru" };
        public string From { get; set; }
        public string To { get; set; }
        public List<string> ToList { get; set; } = new List<string>() { "zaazaa@yandex.ru", "putin@kremlin.ru" };
        public string Subject { get; set; } = "Hello";

        public string Adress { get; set; }

        public string Login { get; set; }


        #region Создание команды - Версия 1
        private void Execute(object obj)
        {
            //Проверяем есть ли такой аккаунт в базе данных
            //Access = EmailSendServiceClass.Check(Body);

            //if (Access)
            {
              //  GetAccess = true;
                MailMessage mm = new MailMessage(From, To, Subject, Body);
                EmailSendServiceClass.Send(mm);
                //Mailer.MainWindow mailerWindow = new Mailer.MainWindow();
            }

        }

        private bool CanExecute(object obj)
        {
            return EmailSendServiceClass.Check(Body);
        }
        

        //Need using System.Windows.Input
        public ICommand Send
        {
            get
            {
                return new DelegateCommand(Execute, CanExecute);
            }
        }
        #endregion

        #region Создание команды - Версия 2 (без использования дополнительных методов)

        //public ICommand ClickAccess
        //{
        //    get
        //    {
        //        return new DelegateCommand((obj) =>
        //            {
        //                Check();
        //                AttemptCount++;
        //            }, (obj) => AttemptCount < 3
        //        ); ;
        //    }
        //}
        #endregion

        #region Публичное свойство для привязки
        public bool Access
        {
            get
            {
                return access;
            }
            set
            {
                if (access != value)
                {
                    access = value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Access"));
                }
            }
        }
        #endregion


    }
}
