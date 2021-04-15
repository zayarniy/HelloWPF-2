using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Mailer.Model
{
    public class Item : INotifyPropertyChanged
    {
        private bool sent;

        public DateTime DateTime { get; set; }

        public MailMessage MailMessage { get; set; }
        public bool Sent
        {
            get => sent; set
            {
                if (sent != value)
                {
                    sent = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Sent"));
                }
            }
        }

        public Item(DateTime dateTime, MailMessage mailMessage)
        {
            this.DateTime = dateTime;
            this.MailMessage = mailMessage;
            this.Sent = true;//Отправлено
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //public override string ToString()
        //{
        //    return DateTime.ToLongDateString() + " " + DateTime.ToLongTimeString() + ":" + MailMessage.Body;
        //}
    }


    public class Database
    {
        public ObservableCollection<Item> Items { get; set; }        

        public Database()
        {
            Items = new ObservableCollection<Item>();
            Items.Add(new Item(DateTime.Now, new MailMessage("from@mail.ru", "to@mail.ru", "subject", "Body1")));
            Items.Add(new Item(DateTime.Now, new MailMessage("from@mail.ru", "to@mail.ru", "subject", "Body2")));
            Items.Add(new Item(DateTime.Now, new MailMessage("from@mail.ru", "to@mail.ru", "subject", "Body3")));
        }



    }

}
