using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
/*
    Ключевые моменты, касающиеся атрибутов .NET:
•	атрибуты представляют собой классы, производные от System.Attribute;
•	атрибуты дают в результате встроенные метаданные;
•	атрибуты в основном бесполезны до тех пор. пока другой агент не проведет в их отношении рефлексию;
•	атрибуты в языке C# применяются с использованием квадратных скобок.
*/

namespace XMLSerializer_List
{
    //Obsolete - поменить тип или член, как устаревший
    //[Obsolete("Don't use it")]//It's the short write ObsoleteAttribute
   // [ObsoleteAttribute("Don't use it")]//Go To Defenition (F12)
    //[Obsolete("Don't use it",true)]//Error if use it
     
    public class Passport
    {
        public int Serial { get; set; }
        public int Number { get; set; }
    }

    [Serializable]
    public class Student
    {
        
        public Passport Passport { get; set; }
       
        public string firstName;
        public string lastName;
        [NonSerialized]        
        int age;


        public Student(string firstName, string lastName, int age)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.age = age;
            this.Passport = new Passport() { Serial = 4305, Number = 121345 };
        }

        public int GetAge()
        {
            return age;
        }
        public Student()
        {
        }

    }


    class Program
    {
        static void SaveAsXmlFormat(List<Student> obj, string fileName)
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(List<Student>));
            //Создаем файловый поток(проще говоря создаем файл)
            Stream fStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            //В этот поток записываем сериализованные данные(записываем xml файл)
            xmlFormat.Serialize(fStream, obj);
            fStream.Close();
        }

        static List<Student> LoadFromXmlFormat(string fileName)
        {
            //Считать класс List<Student> из файла fileName формата XML
            //Обратите внимание, что этот пример показывает нам, что List<Student> не более чем класс
            //Просто его структура более сложная и для ее понимания потребуется некоторый опыт
            XmlSerializer xmlFormat = new XmlSerializer(typeof(List<Student>));
            Stream fStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            List<Student> obj =(List<Student>)xmlFormat.Deserialize(fStream);
            fStream.Close();
            return obj;
        }

        static void Main(string[] args)
        {
            List<Student> list= new List<Student>();
            list.Add(new Student("Иван", "Иванов", 20));
            list.Add(new Student("Петр", "Петров", 21));
            SaveAsXmlFormat(list, "data.xml");
            list=LoadFromXmlFormat("data.xml");
            foreach(var v in list)
            {
                Console.WriteLine("{0} {1} {2}",v.firstName, v.lastName, v.GetAge());
            }
            Console.ReadKey();
        }
    }
}
