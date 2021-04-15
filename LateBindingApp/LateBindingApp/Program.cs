using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace LateBindingApp
{
    // This program will load an external library, 
    // and create an object using late binding.
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with Late Binding *****\n");
            // Try to load a local copy of CarLibrary.
            Assembly a = null;
            try
            {
                //a = Assembly.Load("ClassLibrary");
                a = Assembly.LoadFrom("ClassLibrary.dll");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            if (a != null)
            {
                CreateUsingLateBinding(a);
                InvokeMethodWithArgsUsingLateBinding(a);
            }

            Console.ReadLine();
        }

        #region Invoke method with no args
        static void CreateUsingLateBinding(Assembly asm)
        {
            try 
            {
                // Get metadata for the MyExternalClass type.
                Type myClass = asm.GetType("ClassLibrary.MyExternalClass");

                // Create the Minivan on the fly.
                object obj = Activator.CreateInstance(myClass);
                Console.WriteLine("Created a {0} using late binding!", obj);

                // Get info for method Get without params  (please, input string after run)
                MethodInfo mi = myClass.GetMethod("Get");

                // Invoke method ('null' for no parameters).
                mi.Invoke(obj, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region Invoke method with args
        static void InvokeMethodWithArgsUsingLateBinding(Assembly asm)
        {
            try
            {
                // First, get a metadata description of the MyExternalClass. 
                Type myClass = asm.GetType("ClassLibrary.MyExternalClass");

                // Now, create the sports car.
                object obj = Activator.CreateInstance(myClass);

                // Invoke TurnOnRadio() with arguments.
                MethodInfo mi = myClass.GetMethod("Print");
                mi.Invoke(obj, new object[] { "Hello reflextion!" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion
    }
}
