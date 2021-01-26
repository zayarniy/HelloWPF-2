using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;//for INotifyPropertyChanged
using System.Linq;
using System.Net.Mail;
using System.Runtime.CompilerServices;//for [CallerMemberName]
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace Mailer.ViewModel
{
    public class ViewModel: INotifyPropertyChanged
    {

        Mailer.Model.DBClass database = new Model.DBClass();

        DelegateCommand SendAll;
        DelegateCommand NextTab;
        DelegateCommand PrevTab;

        public ViewModel()
        {
            SendAll = new DelegateCommand(AllMailSendOverThread, CanAllMailSend);
            NextTab = new DelegateCommand((obj) =>
              {
                  Console.WriteLine("Next");
                  TabSwitcherControl tsc = (obj as TabSwitcherControl);
                  TabControl tc = (tsc.CommandParameter as TabControl);
                  tc.SelectedIndex = (tc.SelectedIndex + 1) % tc.Items.Count;
              });
            PrevTab = new DelegateCommand((obj) =>
            {
                Console.WriteLine("Prev");
                TabSwitcherControl tsc = (obj as TabSwitcherControl);
                TabControl tc = (tsc.CommandParameter as TabControl);
                if (tc.SelectedIndex == 0) tc.SelectedIndex = tc.Items.Count - 1;
                else tc.SelectedIndex--;     
            });
        }

        private ObservableCollection<string> senderList = new ObservableCollection<string>() { "zaazaa@yandex.ru","none1@none.ru" };

        public ObservableCollection<string> Senders
        {
            get { return senderList; }
        }

        public void AddSender(string sender)
        {
            senderList.Add(sender);
        }



        public IEnumerable<string> SMTPHosts 
        { 
            get
            {
                return from c in  Model.ServiceData.SmtpClients select c.Host;
            }
        }

        public string SMTPClient { get; set; } = "smtp.yandex.ru";
        public string UserName { get; set; } = "zaazaa@yandex.ru";
        public int Port { get; set; } = 25;
        public string From { get; set; } = "zaazaa@yandex.ru";
        public string To { get; set; } = "zaazaa@yandex.ru";
        public string Subject { get; set; } = "No subject";

        string status="";

        public string Status { get
            {
                return status;
            }
            set
            {
                status = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Status"));
            }
        }

        string log;

        public string Log
        {
            get
            {
                return log;
            }
            set 
            {
                log = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Log"));
            } 
        }
        
        //public FlowDocument RtbDocument { get; set; }

        public string Password { get; set; } = "123456";


        public IEnumerable<Email> Emails
        {
            get
            {
                //return from c in database.Data.Emails select c;
                return senderList.Select(new Func<string, Email>((s) => new Email() { Id = 0 ,Value=s,Name=""})); 
            }
        }

        public IEnumerable<string> Names
        {
            get
            {
               // return from c in database.Data.Emails select c.Value;
                return from c in senderList select c;
            }
        }



        //реализация интерфейса INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ClickNext => NextTab;

        public ICommand ClickPrev => PrevTab;

        public ICommand ClickSendAll
        {
            get
            {
                return SendAll;
            }
        }

        public void AllMailSend(object obj)
        {
            Console.WriteLine("Данные взяли из полей настроек");
            EMailInfo eMailInfo = new EMailInfo();
            eMailInfo.From = From;
            eMailInfo.Password = Password;
            eMailInfo.Port = Port;
            eMailInfo.Sender = From;
            eMailInfo.Subject = "Subject";
            eMailInfo.SmtpClient = SMTPClient;
            eMailInfo.To = To;
            EMailSendServiceClass emailSender = new EMailSendServiceClass();
            emailSender.SendMails(Emails, eMailInfo);
            Status = emailSender.Status;
            if (emailSender.Status!="OK")
                Log += DateTime.Now.ToLongDateString()+" "+DateTime.Now.ToLongTimeString()+"\r\n"+ emailSender.ErrorInfo+"\r\n";
           
        }

        public void AllMailSendOverThread(object obj)
        {
            Task task = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Данные взяли из полей настроек");
                EMailInfo eMailInfo = new EMailInfo();
                eMailInfo.From = From;
                eMailInfo.Password = Password;
                eMailInfo.Port = Port;
                eMailInfo.Sender = From;
                eMailInfo.Subject = "Subject";
                eMailInfo.SmtpClient = SMTPClient;
                eMailInfo.To = To;
                EMailSendServiceClass emailSender = new EMailSendServiceClass();
                emailSender.SendMails(Emails, eMailInfo);
                Status = emailSender.Status;
                if (emailSender.Status != "OK")
                    Log += DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "\r\n" + emailSender.ErrorInfo + "\r\n";
            });           

        }

        public bool CanAllMailSend(object obj)
        {
            if (string.IsNullOrEmpty(UserName))
            {
                Status = "Выберите отправителя";
                return false;
            }
            if (string.IsNullOrEmpty(Password))
            {
                Status = "Укажите пароль отправителя";
                return false;
            }
            return true;
        }

    }
}
