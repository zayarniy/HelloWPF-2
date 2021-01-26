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

       
        public Man Man { get; set; } = new Man() { Field2 = 25, Name = "Ivan" };
     
        public MainWindow()
        {
            InitializeComponent();
        }

        //Событие ErrorEvent является маршрутизируемым событием
        //Происходит при возникновении ошибки проверки в связанном элементе, 
        //но только для привязок со значением NotifyOnValidationError, равным true.
        private void TextBox_Error(object sender, ValidationErrorEventArgs e)
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
    }
}
