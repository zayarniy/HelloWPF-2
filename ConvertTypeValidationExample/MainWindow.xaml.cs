using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConvertTypeValidationExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

       
        public Man Man { get; set; } = new Man() { Field2 = 25 };
     
        public MainWindow()
        {
            InitializeComponent();
            //Пример использования UpdateSourceExceptionFilter
            BindingExpression myBindingExpression = tbField6.GetBindingExpression(TextBox.TextProperty);
            Binding myBinding = myBindingExpression.ParentBinding;
            myBinding.UpdateSourceExceptionFilter = new UpdateSourceExceptionFilterCallback(FilterCallback);
            myBindingExpression.UpdateSource();

        }

        //Метод, предоставляющий пользовательскую логику для обработки исключений, которые механизм привязки находит при обновлении значения источника привязки.
        object FilterCallback(object o,Exception e)
        {
            Console.WriteLine("UpdateSourceExceptionFilterCallBack");
            return "This is from the UpdateSourceExceptionFilterCallBack.";
        }


        //Событие ErrorEvent является маршрутизируемым событием
        //Происходит при возникновении ошибки проверки в связанном элементе, 
        //но только для привязок со значением NotifyOnValidationError, равным true.
        private void Field5_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                Console.WriteLine(sender);
                ((Control)sender).ToolTip = e.Error.ErrorContent.ToString();
            }//если ValidationErrorEventAction.Removed
            else
            {
                Console.WriteLine("Отключили событие");
                ((Control)sender).ToolTip = "";
            } 

        }

        private void Field3_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                Console.WriteLine(sender);
                ((Control)sender).ToolTip = e.Error.ErrorContent.ToString();
            }//если ValidationErrorEventAction.Removed
            else
            {
                Console.WriteLine("Отключили событие");
                ((Control)sender).ToolTip = null;
            }

        }
    }
}
