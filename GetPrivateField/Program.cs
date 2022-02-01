using System;

namespace GetPrivateField
{
    
    class MyClass
    {
        private int a=5;

        public void Print()
        {
            Console.WriteLine(a);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Type type = typeof(MyClass);
            MyClass my = new MyClass();
            System.Reflection.FieldInfo fi =type.GetField("a",System.Reflection.BindingFlags.Instance |
                                                          System.Reflection.BindingFlags.NonPublic);
            Console.WriteLine(fi.GetValue(my));
            fi.SetValue(my, 10);
            my.Print();
            Console.ReadKey();
        }
    }
}
