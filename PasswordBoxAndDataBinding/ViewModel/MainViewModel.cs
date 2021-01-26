using FunctionalFun.UI;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PasswordBoxAndDataBinding.Model;
using System;

namespace PasswordBoxAndDataBinding.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        public MainViewModel()
        {
            this.ClickAccess = new RelayCommand(Check, CanExecute);
        }
        
        public int AttemptCount { get; private set; } = 0;
        public Account Account { get; private set; } = new Account();
        private bool CanExecute()
        {
            return AttemptCount < 3; 
        }

        private void Check()
        {
            AttemptCount++;
            Console.WriteLine(AttemptCount);
           if (Account.Login=="root" && Account.Password=="root")
            {
                Console.WriteLine("Access allowed");
            }
        }

        public RelayCommand ClickAccess { get; private set; }


    }
}