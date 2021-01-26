using Mailer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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

namespace Mailer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel.ViewModel ViewModel { get; set; } = new ViewModel.ViewModel();
        public MainWindow()
        {
            InitializeComponent();
            EMailSendServiceClass.Action += EMailSendServiceClass_Action;
            this.DataContext = ViewModel;

        }

        private void EMailSendServiceClass_Action(string message)
        {
            tbLog.Text += DateTime.Now + ":" + message + "\r\n";
            Console.WriteLine(DateTime.Now + ":" + message + "\r\n");
        }

        private void btnSendAtOnce_Click(object sender, RoutedEventArgs e)
        {
            //string strLogin = cbSenderSelect.Text;
            //string strPassword = cbSenderSelect.SelectedValue.ToString();
            string strLogin = tbUserName.Text;
            string strPassword = tbPassword.Password;
            Console.WriteLine("Данные взяли из полей настроек");
            if (string.IsNullOrEmpty(strLogin))
            {
                MessageBox.Show("Выберите отправителя");
                return;
            }
            if (string.IsNullOrEmpty(strPassword))
            {
                MessageBox.Show("Укажите пароль отправителя");
                return;
            }

            //            EMailInfo eMailInfo = new EMailInfo();
            //MailMessage mm = new MailMessage(cbFrom.Text, cbTo.Text, tbSubject.Text, tbBody.Text);

            //EMailSendServiceClass emailSender = new EMailSendServiceClass();
            //emailSender.Send(mm, System.IO.File.ReadAllText("C:\\Temp\\1.txt"), 587);
        }

        private void btnSend2_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
