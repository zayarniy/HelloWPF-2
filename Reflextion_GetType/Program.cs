using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflextion_GetType
{
    public class MySuperClass
    {
        enum MyEnum
        {
            First,Second, Third
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            int i=0;
            Type type = i.GetType();
            Type type1 = typeof(int);
            //typeName - System.Int32
            //throwOnError - false - не выбрасываем исключение, если тип не найден
            //ignoreCase - true
            Type type2 = Type.GetType("System.Int32", false, true);
            /*
В приведенном выше примере обратите внимание на то, что в строке, передаваемой методу GetType (), никак не упоминается сборка, внутри которой содержится интересующий тип. В этом случае делается предположение о том, что тип определен внутри сборки, выполняющейся в текущий момент. Тем не менее, когда необходимо получить метаданные для типа из внешней закрытой сборки, строковый параметр форматируется с использованием полностью заданного имени типа, за которым следует запятая и дружественное имя сборки, содержащей данный тип:*/
            Type type3 = Type.GetType("Reflextion_GetType.MySuperClass, Reflextion_GetType", true, true);
            Type type4 = Type.GetType("Reflextion_GetType.MySuperClass+MyEnum, Reflextion_GetType", true, true);
            Console.WriteLine("Press any key");
            Console.Read(); 

        }
    }
}
