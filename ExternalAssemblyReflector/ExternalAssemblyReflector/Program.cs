﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Reflection;
using System.IO;  // For FileNotFoundException definition.
//утилита sn - позволяет получить Public Key Token
namespace ExternalAssemblyReflector
{
    class Program
    {
        #region Helper function
        static void DisplayTypesInAsm(Assembly asm)
        {
            Console.WriteLine("\n***** Types in Assembly *****");
            Console.WriteLine("->{0}", asm.FullName);
            Type[] types = asm.GetTypes();
            foreach (Type t in types)
                Console.WriteLine("Type: {0}", t);
            Console.WriteLine("");
        }
        #endregion

        static void Main(string[] args)
        {
            Console.WriteLine("***** External Assembly Viewer *****");

            string asmName = "";
            Assembly asm = null;

            do
            {
                Console.WriteLine("\nEnter an assembly to evaluate");
                Console.Write("or enter Q to quit: ");

                // Get name of assembly.
                asmName = Console.ReadLine();

                // Does user want to quit?
                if (asmName.Equals("Q",StringComparison.OrdinalIgnoreCase))
                {
                    
                    break;
                }

                // Try to load assembly.
                try
                {                    
                    asmName = "TabSwitcher";//Лежит в папке рядом
                    //Load - имя сборки
                    //asm = Assembly.Load(asmName);
                    //LoadFrom - имя файла
                    asm = Assembly.LoadFrom(asmName+".dll");
                    DisplayTypesInAsm(asm);
                }
                catch
                {
                    Console.WriteLine("Sorry, can't find assembly.");
                }
            } while (true);
        }
    }
}
