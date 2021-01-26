using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace FunWithCSharpAsync2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnButton_Click(object sender, RoutedEventArgs e)
        {
            tbText.Text = "Method 1 was started";
            await Task.Run(() => Thread.Sleep(10000));
            tbText.Text = "Method 1 done!";//Сработает, так как обращение к элементам происходят в том же потоке
            //MessageBox.Show("Done!");
        }


        #region То же самое но разделенное на два метода
        private async void btnButton_Click2(object sender, RoutedEventArgs e)
        {
            tbText.Text = "Method 2 was started";
            await MethodReturningVoidAsync();
            tbText.Text = "Method 2 done!";//Сработает, так как обращение к элементам происходят в том же потоке
            //MessageBox.Show("Done!");
        }


        //Метод, который выполняет какую-либо работу
        private async Task MethodReturningVoidAsync()
        {
            await Task.Run(() =>
            {
                int N = 1000000000;
                for (int i = 0; i < N; i++) 
                {
                    if (i % 10000000 == 0)
                    {
                        Console.WriteLine(i);//Do work
                        //tbText.Text = i.ToString();//Не сработает! 
                        this.Dispatcher.Invoke(() => tbText.Text = i.ToString());//Нужно обращаться через диспетчер потоков

                    }
                };
            }
            );
        }
        #endregion
    }
}
