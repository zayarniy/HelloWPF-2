using System;
/*
    Ключевые моменты, касающиеся атрибутов .NET:
•	атрибуты представляют собой классы, производные от System.Attribute;
•	атрибуты дают в результате встроенные метаданные;
•	атрибуты в основном бесполезны до тех пор. пока другой агент не проведет в их отношении рефлексию;
•	атрибуты в языке C# применяются с использованием квадратных скобок.
*/
namespace OurAttribute
{
    //Нужно перейти в папку с проектом и запустить IL DASM, чтобы продемонстрировать
    //как атрибуты хранятся в информации о сборке
    //желательно, в целях безопасности, сделать атрибут запечатаным

    public sealed class DescriptionAttribute:System.Attribute
    {
        public string Description { get; set; }
        public DescriptionAttribute(string description) => Description = description;
        public DescriptionAttribute(){ Description = "Empty Info"; }
    }

    #region Наложение ограничений на использование атрибута
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Delegate)]
    public sealed class DescriptionAttribute2 : System.Attribute
    {
        public string Description { get; set; }
        public DescriptionAttribute2(string description) => Description = description;
        public DescriptionAttribute2() { }
    }
    #endregion

    [Obsolete("Don't use it")]
    [Description("It needs to show serialization")]
    public class Passport
    {
        public int Serial { get; set; }
        public int Number { get; set; }
    }

    [Serializable]    
    [Description("Serialization example")]
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
}
