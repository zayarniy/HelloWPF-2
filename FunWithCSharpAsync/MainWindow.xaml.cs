﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FunWithCSharpAsync
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnButton_Click(object sender, RoutedEventArgs e)
        {
            tbText.Text = DoWork();
        }

        private async void btnButton_Click2(object sender, RoutedEventArgs e)
        {
            //Имена асинхронных рекомендуется заканчивать на Async, так как они возвращают объект Task
            tbText2.Text =await DoWorkAsync();
            /*
            Ключевое слово await отвечает за извлечение внутреннего возвращаемого значения, содержащегося в объекте Task. Из-за отсутствия этого ключевого слова возникает несовпадение типов. 
            */

        }

        //Некоторая работа
        private string DoWork()
        {
            //tbText.Text = "Do work";
            Thread.Sleep(10000);
            return "Done with work!";
        }

        private Task<string> DoWorkAsync()
        {
            //Не сработает, так как произведена попытка доступа из другого потока!!!!
            //tbText.Text = "Do work";
             
            //Создаем задачу через лябда-выражение и тут же запускаем ее на выполнение
            //метод возвращает ссылку на объект с потоком выполнения, в котором так же храниться результат выполнения задачи
            return Task.Run(() =>
            {
                //Thread.Sleep(10000);
                //return "Done with work!";
                return DoWork();
            });
            //другой способ создание и запуск задачи
            Task<string> task = new Task<string>(DoWork);
            task.Start();
            return task;

        }

        //Кроме того, метод DoWork () может также быть декорирован с помощью ключевых слов async и await (хотя это не является обязательным).
        //Если в коде используется await, то код должен быть объявлен со словом async

        private async Task<string> DoWorkAsync2()
        //private Task<string> DoWorkAsync2()//Код не скомпилируется, если встречено слово await, но нет слова async
        {
            //Создаем задача через лябда-выражение и тут же запускаем ее на выполнение
            //метод возвращает ссылку на объект с потоком выполнения, в котором так же храниться результат выполнения задачи

            //Не сработает, так как произведена попытка доступа из другого потока!!!!
            // tbText.Text = "Do work";
            return await Task.Run(() =>
            {
                Thread.Sleep(10000);
                return "Done with work!";
            });
            //другой способ создание и запуск задачи
            Task<string> task = new Task<string>(DoWork);
            task.Start();
            return await task;

        }

    }
}
/*
Первым делом, обратите внимание, что обработчик события Click кнопки помечен ключевым словом async. Это значит, что данный метод должен вызываться в неблокирующей манере. Кроме того, в реализации обработчика события перед именем вызываемого метода используется ключевое слово await. Это важный момент: если метод декорируется ключевым словом async, но не имеет хотя бы одного внутреннего вызова метода с применением await, получается блокирующий, синхронный вызов (на самом деле в таком случае компилятор выдаст соответствующее предупреждение).
Мы должны были воспользоваться классом Task из пространства имен System. Threading .Tasks для проведения рефакторинга метода DoWork () с целью обеспечения его корректного функционирования. В сущности, вместо возврата специфического значения напрямую (объекта string в текущем примере) мы возвращаем объект Task<T>, где обобщенный параметр типа Т — это лежащее в основе возвращаемое значение.
Реализация DoWork () теперь непосредственно возвращает объект Task<T>, который является возвращаемым значением Task. Run (). Метод Run () принимает делегат Funco или ActionO и, как уже известно к этому моменту, реализацию можно упростить за счет применения лямбда-выражения. В целом новая версия DoWork () обладает следующими характеристиками.
При вызове запускается новая задача. Эта задача заставляет вызывающий поток уснуть на 10 секунд. По завершении вызывающий поток предоставляет строковое возвращаемое значение. Эта строка помещается в новый объект Task<string> и возвращается вызывающему коду.
Благодаря этой новой реализации метода DoWork (), мы можем получить некоторое представление о действительной роли ключевого слова await. Оно всегда будет модифицировать метод, который возвращает объект Task.
Когда поток выполнения достигает await, вызывающий поток приостанавливается до тех пор, пока вызов не будет завершен. Запустив эту версию приложения, вы обнаружите, что можно щелкнуть на кнопке и сразу же вводить в текстовом поле. Спустя 10 секунд заголовок окна будет обновлен сообщением о завершении работы.
*/