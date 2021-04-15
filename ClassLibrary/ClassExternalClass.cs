using System;

namespace ClassLibrary
{
    interface IMyInterface
    {
        string Get();
    }

    public class MyExternalClass: IMyInterface
    {
        public const double PI = 3.1415;
        public string String1 { get; private set; } = "Very important data";
        string data;
         

        public MyExternalClass(string str)
        {
            String1 = str;
        }

        public MyExternalClass()
        {

        }


        public void Print(string message)
        {
            Console.WriteLine(message);
        }

        public string Get()
        {
            data=Console.ReadLine();
            return data;
        }

    }
}
