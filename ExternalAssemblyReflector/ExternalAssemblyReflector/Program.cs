using System;
 
using System.Reflection;
using System.IO;  // For FileNotFoundException definition.
//утилита sn - позволяет получить Public Key Token
/*
PublicKeyToken - уникальный идентификатор сборки, выдается при помещении в GAC (Global Assembly Cache)

Задает токен открытого ключа сборки, представляющий собой последние 8 байтов хэша SHA-1 открытого ключа, с помощью которого подписана сборка.

Culture - строка, указывающая язык и региональные параметры сборки. Пустая строка указывает на инвариантный язык и региональные параметры.
 
*/
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
                Console.WriteLine("\nEnter an assembly to evaluate (Enter - assembly by default)");
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
                    if (asmName=="") asmName = "TabSwitcher";//Лежит в папке рядом
                    //Load - имя сборки
                    asm = Assembly.Load(asmName);//System
                    //LoadFrom - имя файла
                    //asm = Assembly.LoadFrom(asmName+".dll");
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
