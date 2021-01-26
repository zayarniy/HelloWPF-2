using GalaSoft.MvvmLight;
//using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MVVMLightToolExample.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MVVMLightToolExample.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<Employee> employees;
        private Employee selectedEmployee;

        public MainViewModel()
        {
            LoadEmployeesCommand = new RelayCommand(LoadEmployeesMethod);
            SaveEmployeesCommand = new RelayCommand(SaveEmployeesMethod);
        }

        public ICommand LoadEmployeesCommand { get; private set; }
        public ICommand SaveEmployeesCommand { get; private set; }

        public ObservableCollection<Employee> EmployeeList
        {
            get
            {
                return employees;
            }
        }

        public Employee SelectedEmployee
        {
            get
            {
                return selectedEmployee;
            }
            set
            {
                selectedEmployee = value;
                RaisePropertyChanged("SelectedEmployee");
            }
        }

        public void SaveEmployeesMethod()
        {
            Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Employees Saved."));
        }

        private void LoadEmployeesMethod()
        {
            employees = Employee.GetSampleEmployees();
            this.RaisePropertyChanged(() => this.EmployeeList);
            Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Employees Loaded."));
        }
    }
}
/*
� ������ ��� �������� � ���� ������ � ������ EmployeeList � SelectedEmployee. EmployeeList ������������� � �������� ItemsSource ������� ListBox � ������ 25, � SelectedEmployee ������������� � �������� SelectedItem ������� ListBox � ������ 25.

����� ����, � ����� ������ ��� ������� � ������ LoadEmployeesCommand � SaveEmployeeCommand. ��� ������� ��������� � �������������� RelayCommand � ������������ ������. RelayCommand - ��� �������, ��������������� MVVM Light Toolkit, ���� ������� - �������� ���� �������������� ����������� ������ �������� ����� ������ ���������. LoadEmployeeCommand ������������� � ������ � ������ 18, � SaveEmployeeCommand ������������� � ������ � ������ 19. LoadEmployeeCommand ��������� LoadEmployeesMethod, � SaveEmployeesCommand ��������� ������� ������ SaveEmployeesMethod.

� ������ Load employee �� ��������� ������ ����������� �� ������������ ������ ������ Employee. ����� �� ���������� ���������������� ��������� � ���, ��� �������� EmployeeList ���� �������� � ������� ������ RaisePropertyChanged.

� ��������� ������ �� ���������� ����������� � ���������������� ���������, ����� ���������� ���� ��������� � ������� ���������� ��������. ��� ����� �� ���������� ����� Messenger MVVM Light Toolkit. ����� Messenger ��������� ������� ������������ ����������� ����� ���������� ��������. �������� Default Messenger ���������� ��������� �� ��������� ������� Messenger, ��������������� ���������������. NotificationMessage ����� ��������������� ��������������� � ������������ ��� �������� ���������� ��������� ����������.
*/
