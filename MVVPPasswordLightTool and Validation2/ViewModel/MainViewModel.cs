using GalaSoft.MvvmLight;
//using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MVVMPasswordLightTool.Model;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media.TextFormatting;

namespace MVVMPasswordLightTool.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            //��� �������������� ������������ � ���������� � MVVM ������������ �������. ��� �� ������, ��� ����� �� ����� ������������ ������� � ���������� ������, ������ �����, ��� ��������, ������ ������� ������� ������������ �������.

            //this.ClickAccess = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(Check, CanExecute);
            this.ClickAccess2 = new RelayCommand(Check, CanExecute);
            this.CloseWindowCommand = new RelayCommand<MainWindow>(this.CloseWindow);

        }


        private void CloseWindow(MainWindow window)
        {
            window?.Close();
        }

        public int AttemptCount { get; private set; } = 0;

        public Account Account { get; set; } = new Account("", "");

        public ICommand ClickAccess { get; private set; }

        public RelayCommand ClickAccess2 { get; private set; }

        public RelayCommand<MainWindow> CloseWindowCommand { get; private set; }

        public ObservableCollection<Account> AccountsList
        {
            get
            {
                return Model.Accounts.ListAccounts;
            }
        }

        void Check()
        {
            AttemptCount++;
            if (Model.Accounts.Checks(Account))
            {
                AttemptCount = -1;
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Access allowed"));
            }
            else
            {
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Access denied"));
            }
            RaisePropertyChanged("AttemptCount");
        }

        bool CanExecute() 
        {
            return AttemptCount < 3 && AttemptCount != -1;
        }

    }
}