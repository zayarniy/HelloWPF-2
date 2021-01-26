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

namespace FunWithCSharpAsync3
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
            await Task.Run(() => { Thread.Sleep(3000); });
            tbText.Text = "Done with first task!";//завершилась первая задача 
            await Task.Run(() => { Thread.Sleep(3000); });
            tbText.Text = "Done with second task!";//завершилась вторая задача
            await Task.Run(() => { Thread.Sleep(3000); });
            tbText.Text = "Done with third task!";//завершилась третья задача
        }
    }
}
/*
  Здесь каждая задача всего лишь приостанавливает текущий поток на некоторый период времени; тем не менее, с помощью этих задач может быть представлена любая единица работы (обращение к веб-службе, чтение базы данных или что-нибудь еще). Ниже перечислены ключевые моменты, связанные с этим примером.
•	Методы (а также лямбда-выражения или анонимные методы) могут быть помечены с помощью ключевого слова a sync, что позволяет методу работать в неблокирующей манере.
•	Методы (а также лямбда-выражения или анонимные методы), помеченные ключевым словом async, будут выполняться в блокирующей манере до тех пор, пока не встретится ключевое слово await.
•	Один метод async может иметь множество контекстов await.
•	Как только встретилось выражение await, вызывающий поток приостанавливается вплоть до завершения ожидаемой задачи. ***Тем временем управление возвращается коду, вызвавшему метод.***
•	Ключевое слово await будет скрывать возвращаемый объект Task, выглядя как прямой возврат лежащего в основе возвращаемого значения. Методы, не имеющие возвращаемого значения, просто возвращают void.
•	По соглашению об именовании методы, которые могут быть вызваны асинхронно, должны быть помечены с помощью суффикса async.
*/
