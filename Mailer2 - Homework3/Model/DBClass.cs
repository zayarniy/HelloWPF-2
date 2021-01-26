using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer.Model
{
    public class DBClass
    {
       public EmailsDataContext Data { get; } = new EmailsDataContext();

        //private List<string> senderList = new List<string>() { "none@gmail.com" };
    }
}
