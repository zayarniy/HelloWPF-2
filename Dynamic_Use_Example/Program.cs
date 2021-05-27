using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamic_Use_Example
{
    //Примев, в котором для различных типов данных нужно вызывать различные обработчики
    class Program
    {
        //Различные данные
        //static public object[] data = { "string value", 100, 3.14, true };

        //Сложности со второй реализацией если нет метода для данных
        static public object[] data = { "string value", 100, 3.14, true,'q',new List<string>() };

        private static void ProcessValues(IEnumerable<object> values)
        {
            #region Вариант 1. Сопоставление с образом
            Console.WriteLine("Сопоставление с образом  (pattern matching)");
            foreach (var value in values)
                switch(value)
                {
                    //Сопоставление с образом  (pattern matching)
                    case string val:ProcessValue(val);break;
                    case int val: ProcessValue(val); break;
                    case bool val: ProcessValue(val); break;
                    case double val: ProcessValue(val); break;
                }
            #endregion
            #region dynamic
            Console.WriteLine("Использование типа dynamic");
            foreach (dynamic value in values)
                ProcessValue(value);
            #endregion
        }

        private static void ProcessValue(string value) => Console.WriteLine("string: {0}",value);

        private static void ProcessValue(int value) => Console.WriteLine("int: {0}", value);

        private static void ProcessValue(bool value) => Console.WriteLine("bool: {0}", value);

        private static void ProcessValue(double value) => Console.WriteLine("double: {0}", value);
        static void Main(string[] args)
        {
            ProcessValues(data);
            Console.ReadKey();
        }
    }
}
