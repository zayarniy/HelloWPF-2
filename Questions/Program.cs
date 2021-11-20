//Возможно этот код лучше показывать в каком-нибудь Notepad++, чтобы компилятор не подсказывал ошибки
//Нужно преварительно скрыть все регионы
using System;

namespace Questions
{
    #region Задача 1

    
    class Interview
    {
        //Существуют класс A: 
        public class ClassA
            {
                public virtual void Method1()
                {
                    Console.WriteLine("ClassA");
                }
            }
            //И класс B - наследник класса A:
            public class ClassB : ClassA
            {
                public override void Method1()
                {
                    Console.WriteLine("ClassB");
                }
            }

            public void Code1()
            {

            /// Какое связывание здесь используется (раннее или позднее)?
            /// Почему здесь ошибка?
           //ClassB object1 = new ClassA();

            #region Можно ли так исправить?
            //ClassB object1 = (new ClassA() as ClassB);
            #endregion
            #region Ответ
            //Нет, будет ошибка приведения типов
            #endregion
            //Какой вывод получим, выполнив следующий код ?

            #region А так?
            ClassA object1 = new ClassB();
            #endregion
            #region Ответ
            //Да, так как потомок может ссылаться на экземляр наследника
            #endregion
            //Какой вывод получим, выполнив следующий код ?Почему?
            object1.Method1();
            #region Ответ
            //Class B
            #endregion
            //Какой вывод получим, выполнив следующий код ?Почему?
            ClassB object2 = new ClassB();
            object2.Method1();
            #region Ответ
            //Class B
            #endregion
           

            //Какой вывод получим, выполнив следующий код ?Почему?
            ClassA object4 = new ClassB();
            (object4 as ClassA).Method1();
            #region Ответ
            //Class B
            #endregion

        }
        #endregion

        #region Задача 2
        // Логика. Каков результат работы следующего кода?
        public void Code2()//Алгебра логики
        {
            bool a = true;//1
            bool b = false;//0
            bool c = !a || (!!a && !b) && (a || b);
            #region Ответ
            c = !true || (!!true && !false) && (true || false);
            c = false || (true && true) && (true || false);
            c = true;
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
            //    Console.WriteLine(value);
            //    value--;
            //}

            #region Ответ
            //Ни разу, так как value - не может быть инициализированна null
            #endregion
        }
        #endregion
        #region Задача 4        
        public void Code4()
        
        {
            //Какие проблемы с этим кодом
            int i = 1;
            object obj = i;//inboxing
            ++i;
            //Console.WriteLine((short)obj);//unboxing
            #region Ответ 1
            #endregion
            #region Ответ 2
            Console.WriteLine((short)(int)obj);//unboxing in int =>short
            #endregion
        }
        #endregion
    }

    class Program
    {



        static void Main(string[] args)
        {
            Interview interview = new Interview();
            interview.Code4();
            //Interview.Question1();
            Console.ReadKey();

        }
    }
}
