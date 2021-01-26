using System;
using System.Collections.Generic;
using System.Linq;
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
}
