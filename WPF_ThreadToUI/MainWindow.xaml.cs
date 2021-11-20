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

namespace WPF_ThreadToUI
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

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            Pressed(btnButton1);
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            //Don't work
            Thread thread;
            thread = new Thread(new ParameterizedThreadStart(Pressed));
            thread.Start(btnButton2);//Attempt to access to UI thread over other thread
            //btnButton2.Content = "Pressed";
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            //Work, but whatever freeze
            Thread thread=new Thread(new ParameterizedThreadStart(PressedOverDispatcher));
            thread.Start(btnButton3);
            btnButton3.Content = "Thread started";
        }

        void Pressed(object btn)
        {
            Thread.Sleep(5000);
            (btn as Button).Content = "Pressed";
        }
        void PressedOverDispatcher(object btn)
        {
            Thread.Sleep(5000);
            (btn as Button).Dispatcher.Invoke(()=>(btn as Button).Content = "Pressed");
        }

        private async void Button_Click4(object sender, RoutedEventArgs e)
        {
            btnButton4.Content = "Press me over Task (work)";
            await PressedOverDispatcherAndTask(btnButton4);

        }

        Task PressedOverDispatcherAndTask(Button btn)
        {
            Task task = new Task(() =>
            {
                Thread.Sleep(5000);
                btn.Dispatcher.Invoke(()=>btn.Content = "Pressed");
            });
            task.Start();            
            return task;
            return Task.Factory.StartNew(() =>
                            {
                                Thread.Sleep(5000);
                                btn.Dispatcher.Invoke(() => btn.Content = "Pressed");
                            });

        }


        //private async void Button_Click4(object sender, RoutedEventArgs e)
        //{
        //    await PressedOverDispatcher(btnButton3);

        //    //System.Threading.Tasks.Task task = new Task(() => {
        //    //    Thread.Sleep(5000);
        //    //    btnButton4.Content = "Pressed";
        //    //    });
        //    //task.Start();
        //}
    }
}
