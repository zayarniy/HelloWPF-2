using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
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

namespace EmailSendExample
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var fromAddress = new MailAddress("geekbrains2021@gmail.com", "Tester");
                var toAddress = new MailAddress("geekbrains2021@gmail.com", "Tester");
                #region password
                const string fromPassword = "";
                #endregion
                //Преобазование из Windows-1251 в UTF-8
                string subject = "Результаты тестирования  ";
                string fio = "from me";
                rtbBody.SelectAll();
                string body = rtbBody.Selection.Text; 

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("geekbrains2021@gmail.com", fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject + fio,
                    Body = body

                })
                {
                    smtp.Send(message);
                }
                MessageBox.Show("Message has sent");

            }
            catch (Exception ex)
            {
                //using System.Diagnostic
                Debug.WriteLine(ex);
                Console.WriteLine(ex);


            }
        }
    }
}
