using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;


namespace XMLSerializer_List
{
    //Помечаются методы или классы, которые не рекомендуется использовать (false - будет ли ошибка при использовании этого атрибута)
    [Obsolete("Don't use it",false)]
   //[ObsoleteAttribute()]
    public class Passport
    {
        public int Serial { get; set; } 
        public int Number { get; set; }
    }

    [Serializable]
    public class Student
    {
        
        public Passport Passport { get; set; }
        //Чтобы поля можно было серилизовать, они должны быть открытыми 
       
        public string firstName;
        public string lastName;
        //Если поле не открыто оно не будет сериализоваться
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
            Stream fStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            xmlFormat.Serialize(fStream, obj);
            fStream.Close();
        }

        static List<Student> LoadFromXmlFormat(string fileName)
        {
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
