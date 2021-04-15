using System;
using System.Collections.Generic;
using System.ComponentModel;//for INotifyPropertyChanged
using System.Linq;
using System.Runtime.CompilerServices;//for [CallerMemberName]
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Password_MVVM.ViewModel
{
    public class ViewModel: INotifyPropertyChanged
    {

        //реализация интерфейса INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        DelegateCommand TextCommand;
        DelegateCommand ClickCommand;

        public ViewModel()
        {
            ClickCommand = new DelegateCommand(Execute, CanExecute);
            //TextCommand = new DelegateCommand(Execute2, CanExecute2);
        }

        //Вынести в отдельный класс

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

        public Model.Account Account { get; set; } = new Model.Account("None", "None");

        Model.Accounts Accounts=new Model.Accounts();

        #region Создание команды - Версия 1
        private void Execute(object obj)
        {            
            Access = Accounts.Checks(Account);
            if (Access)
            {
                GetAccess = true;
                //Mailer.MainWindow mailerWindow = new Mailer.MainWindow();
            }

            AttemptCount++;
        }

        private bool CanExecute(object obj)
        {
            return AttemptCount < 3;
        }

        public ICommand ClickAccess
        {
            get
            {
                return ClickCommand;                
            }
        }
        #endregion

        public ICommand TextCmd
        {
            get
            {
                Console.WriteLine("Произошла привязка");
                return TextCommand;
            }
        }
        #region Создание команды - Версия 2

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
        private void Execute2(object obj)
        {
            //Access = Accounts.Checks(Account);
            //AttemptCount++;
            Console.WriteLine("1");
        }

        private bool CanExecute2(object obj)
        {
            return Account.Password.Length < 20;
        }

    }
}
