using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using PrismMVVMTestProject.Views;


namespace PrismMVVMTestProject.ViewModels
{
    class TestViewModel: BindableBase
    {
        public ICommand ShowCommand
        {
            get;
            private set;
        }
        public TestViewModel()
        {
            ShowCommand = new DelegateCommand(ShowMethod);
        }

        private void ShowMethod()
        {
            PopUpWindow objPopupwindow = new PopUpWindow();
            objPopupwindow.Show();
        }
    }
}
