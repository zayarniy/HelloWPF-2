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

//Пример работы маршрутизируемого события
namespace FileInputBoxExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private void fibUI_FileNameChanged(object sender, EventArgs e)
        {
            Console.WriteLine(sender);
        }

        private void fibUI_FileNameChanged(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Прямое событие:"+sender);
        }

        private void StackPanel_FileNameChanged(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Всплывающее событие:" + sender);

        }
    }
}
