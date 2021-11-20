using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
using System.Reflection;
using System.IO;

//разделяемая сборка - те, которыми пользуются другие приложения (например System.Windows.Forms)
//https://metanit.com/sharp/tutorial/10.2.php

namespace SharedAsmReflector
{
    public class SharedAsmReflector
    {
        #region DisplayInfo helper method
        private static void DisplayInfo(Assembly a)
        {
            Console.WriteLine("***** Info about Assembly *****");
            Console.WriteLine("Loaded from GAC? {0}", a.GlobalAssemblyCache);
            Console.WriteLine("Asm Name: {0}", a.GetName().Name);
            Console.WriteLine("Asm Version: {0}", a.GetName().Version);
            Console.WriteLine("Asm Culture: {0}",
              a.GetName().CultureInfo.DisplayName);
            Console.WriteLine("\nHere are the public enums:");

            // Use a LINQ query to find the public enums.
            Type[] types = a.GetTypes();
            var publicEnums = from pe in types
                              //where pe.IsClass 
                              //&& pe.IsPublic//Только открытые перечисления
                              select pe;
            int i = 0;
            foreach (var pe in types)
            {
                i++;
                Console.WriteLine(i+"."+pe);
                
            }
        }
        #endregion

        static void Main(string[] args)
        {
            Console.WriteLine("***** The Shared Asm Reflector App *****\n");

            // Load System.Windows.Forms.dll from GAC.
            string displayName = null;
            displayName = "System.Windows.Forms,"+
              "Version=4.0.0.0," +
              "PublicKeyToken=b77a5c561934e089," +
              @"Culture=""";
            /*
            При создании отображаемого имени соглашение PublicKeyToken=null отражает тот факт, что требуется связывание и сопоставление со сборкой, не имеющей строгого имени. Вдобавок Culture="" указывает, что сопоставление должно осуществляться со стандартной культурой целевой машины
            */
            Assembly asm = Assembly.Load(displayName);
             
            DisplayInfo(asm);
            Console.WriteLine("Done!");
            Console.ReadLine();
        }
    }
}
