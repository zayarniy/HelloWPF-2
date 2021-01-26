using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer
{
    public class DBClass
    {
        private EmailsDataContext emails = new EmailsDataContext();

        public IQueryable<Email> Emails
        {
            get
            {
                return from c in emails.Emails select c;
            }
        }

        public void Update()
        {
            emails.SubmitChanges();
        }
    }
}
