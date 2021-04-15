using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CommandsExample.ViewModel
{
    public class ViewModel
    {

        //private Commands command;



        #region Вариант 1
        public DelegateCommand Command1
        {
            get
            {
                //var command = new DelegateCommand((o) => { MessageBox.Show("Команда 1"); },CanClick);
                ////var command = new DelegateCommand(Execute, CanClick);
                return new DelegateCommand(Execute, CanClick); 
            }
        }

        void Execute(object o)
        {
            MessageBox.Show(o as string);
        }

        bool CanClick(object o)
        {
            if (o != null)
                return (o as string).Contains("t");
            else return false;
        }


        #endregion

        #region Вариант 2
        public DelegateCommand Command2 => new DelegateCommand((o) => { MessageBox.Show("Команда 2"); });

        #endregion
    }
}
