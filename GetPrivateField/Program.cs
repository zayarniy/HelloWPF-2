using System;

namespace GetPrivateField
{
    
    class MyClass
    {
        private int a=5;
    }
    class Program
    {
        static void Main(string[] args)
        {
            Type type = typeof(MyClass);
            MyClass my = new MyClass();
            var fi=type.GetField("a",System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            Console.WriteLine(fi.GetValue(my));
            Console.ReadKey();
        }
    }
}
