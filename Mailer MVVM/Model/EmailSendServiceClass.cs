using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Mailer.Model
{

    //Альтернативный класс для отправки
    static class ServiceData
    {
        //static public List<string> SmptClients = new List<string>()
        //{ "smtp.yandex.ru", "smtp.gmail.com","smtp.mail.ru" };
        //    static public List<int> Ports = new List<int>() { 25, 58, 25 };
        static public List<SmtpClient> SmtpClients { get; } = new List<SmtpClient>() { new SmtpClient("smtp.yandex.ru", 25), new SmtpClient("smtp.gmail.com", 58), new SmtpClient("smtp.mail.ru", 25) };

    }


    class EMailSendServiceClass 
    {


        static public event Action<string> Action;//Любое событие может вызываться только внутри класса в котором оно определено
        

        public void Send(MailMessage message)//, string password, int port)
        {
            try
            {
                //Password = password;
                //Port = port;
                //var fromAddress = new MailAddress("geekbrains2021@gmail.com", "Tester");
                //var toAddress = new MailAddress("geekbrains2021@gmail.com", "Tester");
                //Преобазование из Windows-1251 в UTF-8
                string subject = message.Subject;// tbSubject.Text;
                //string fio = "from me";
                //rtbBody.SelectAll();
                string password = System.IO.File.ReadAllText("C:\\temp\\1.txt");
                int port=587;
                string body = message.Body;
                var smtp = new SmtpClient()
                {
                    Host = "smtp.gmail.com",
                    Port = port,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("geekbrains2021@gmail.com", password)
                };
                Action?.Invoke("Message is sending");
                smtp.Send(message);
                //MessageBox.Show("Message has sent");
                //Debug.WriteLine("Message has sent");
                Action?.Invoke("Message has sent");
            }
            catch (Exception ex)
            {
                //using System.Diagnostic
                Debug.WriteLine(ex);
                //Console.WriteLine(ex);
                Action?.Invoke(ex.Message);
            }        

        }

        public bool Check(string body)
        {
            return !(String.IsNullOrEmpty(body));
        }
    }
}
