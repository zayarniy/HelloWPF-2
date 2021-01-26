using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questions
{
    #region Задача 1

    class Interview
    { 
    //Существуют классы:
    public class ClassA
    {
        public virtual void ClassMethod()
        {
            Console.Write("ClassA");
        }
    }
    public class ClassB : ClassA
    {
        public override void ClassMethod()
        {
            Console.Write("ClassB");
        }
    }

        /// <summary>
        /// Какой вывод получим, выполнив следующий код?
        /// </summary>
        public void Code1()
        {
            //ClassB object1 = new ClassA();//Ошибка приведения типов
            ClassB object1 = (ClassB)new ClassA();//Исправление
            object1.ClassMethod();
            ClassB object2 = new ClassB();
            object2.ClassMethod();
            ClassA object3 = new ClassB();
            object3.ClassMethod();
        }
        #endregion

        #region Задача 2
        /// <summary>
        /// Логика. Каков результат работы следующего кода?
        /// </summary>
        public void Code2()
        {
            bool a = true;
            bool b = false;
            bool c = !a || (!!a && !b) && (a || b);
            #region Ответ
            c = !true || (!!true && !false) && (true || false);
            bool c1 = !a || !b;//Упрощенное выражение c
            #endregion
            Console.Write(c);
            Console.Write(c1);
        }
        #endregion

        #region Задача 3
        /// <summary>
        /// Сколько раз выполнится цикл?    
        /// </summary>
        public void Code3()
        {
            //var value = null;
            //value = 10;
            //while (value > 0)
            //{
            //    Console.WriteLine(a);
            //    value--;
            //}

            #region Ответ
            //Ни разу, так как alue - не может быть инициализированна null
            #endregion
        }
        #endregion
        #region Задача 4
        //Какие проблемы с этим кодом
        public static void Question1()
        {
            int i = 1;
            object obj = i;
            ++i;
            Console.WriteLine(i);
            Console.WriteLine(obj);
            Console.WriteLine((short)obj);

            #region Ответ
            Console.WriteLine((short)(int)obj);
            #endregion
        }
        #endregion
    }

    class Program
    {



        static void Main(string[] args)
        {
            Interview interview = new Interview();
            interview.Code2();
            Console.ReadKey();

        }
    }
}
