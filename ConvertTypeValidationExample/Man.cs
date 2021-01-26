using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertTypeValidationExample
{
    //public class Man
    //{
    //    public string Name { get; set; }
    //    public int Age { get; set; }
    //}

    //статья 
    //https://docs.microsoft.com/ru-ru/archive/msdn-magazine/2010/june/msdn-magazine-input-validation-enforcing-complex-business-data-rules-with-wpf
    public class Man : IDataErrorInfo
    {

        public string Name { get; set; }

        public int age1;
        public int Field0 { get; set; }

        string stringField;
        public string StringField
        {
            get { return  stringField; }
            set
            {
                if (value.Length>5)
                 throw new Exception(); else stringField = value;
            }
        }
        //public int Age1 { get; set; }
        public int Field2 { get; set; }
        public int Field3 { get; set; }

        public int Field4 { get; set; }

        public int Field5 { get; set; }

        public string this[string columnName]
        {
            get
            {
                string Error = String.Empty;
                switch (columnName)
                {
                    case "Field4":
                        if ((Field4 < 0) || (Field4 > 100))
                        {
                            Error = "Поле Field2 должен быть больше 0 и меньше 100";
                            Console.WriteLine(Error);
                        }
                        break;
                    case "Name":
                        //Обработка ошибок для свойства Name
                        Error = "Нет ошибок";
                        Console.WriteLine(Error);
                        break;
                }
                return Error;//Если возвращается не пустое значение - означает ошибку
            }        
        }

        public string Error { get; private set; }// => throw new NotImplementedException();
    }
}
