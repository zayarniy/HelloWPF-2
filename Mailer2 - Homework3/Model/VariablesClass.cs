﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using GeekBrainTools;

namespace Mailer.Model
{
    public static class VariablesClass
    {
        public static Dictionary<string, string> Senders
        {
            get { return dicSenders; }
        }

        private static Dictionary<string, string> dicSenders = new Dictionary<string, string>()
        {
            { "zaazaa@yandex.ru","1234"/*PasswordClass.getPassword("1234l;i") */},
            { "sok74@yandex.ru","4321"/*PasswordClass.getPassword(";liq34tjk")*/ }
        };
    }


}

