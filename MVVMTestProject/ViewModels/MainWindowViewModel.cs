using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MVVMTestProject.Commands.Base;
using MVVMTestProject.ViewModels.Base;
using MVVMTestProject.Views;


namespace MVVMTestProject.ViewModels
{
    class MainWindowViewModel: ViewModel
    {
        public ICommand ShowCommand
        {
            get;
            private set;
        }

        public MainWindowViewModel()
        {
            ShowCommand = new RelayCommand(ShowMethod);
        }

        void ShowMethod(object p)
        {
            PopUpWindow objPopupwindow = new PopUpWindow();
            objPopupwindow.Show();
        }


    }
}
