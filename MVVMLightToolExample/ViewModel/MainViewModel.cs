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
Я создал два свойства в этом классе с именем EmployeeList и SelectedEmployee. EmployeeList привязывается к свойству ItemsSource объекта ListBox в строке 25, а SelectedEmployee привязывается к свойству SelectedItem объекта ListBox в строке 25.

Кроме того, я также создал две команды с именем LoadEmployeesCommand и SaveEmployeeCommand. Обе команды создаются с использованием RelayCommand в конструкторе класса. RelayCommand - это команда, предоставляемая MVVM Light Toolkit, цель которой - передать свои функциональные возможности другим объектам путем вызова делегатов. LoadEmployeeCommand привязывается к кнопке в строке 18, а SaveEmployeeCommand привязывается к кнопке в строке 19. LoadEmployeeCommand выполняет LoadEmployeesMethod, а SaveEmployeesCommand выполняет нажатие кнопки SaveEmployeesMethod.

В методе Load employee мы загружаем список сотрудников из статического метода класса Employee. Далее мы уведомляем пользовательский интерфейс о том, что свойство EmployeeList было изменено с помощью метода RaisePropertyChanged.

В последнем случае мы отправляем уведомление в пользовательский интерфейс, чтобы отобразить окно сообщения с текстом «Сотрудник загружен». Для этого мы используем класс Messenger MVVM Light Toolkit. Класс Messenger позволяет объекту обмениваться сообщениями между различными классами. Свойство Default Messenger использует экземпляр по умолчанию объекта Messenger, предоставляемый инструментарием. NotificationMessage также предоставляется инструментарием и используется для передачи строкового сообщения получателю.
*/
