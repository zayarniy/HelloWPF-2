using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

//Включение в пользовательский элемент управления маршрутизируемых событий
namespace MyLib
{
    [ContentProperty("FileName")]//Показать заменой Content
    public partial class FileInputBox : UserControl
    {
        public FileInputBox()
        {
            InitializeComponent();
            theTextBox.TextChanged += new TextChangedEventHandler(OnTextChanged);
        }

        private void theButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            if (d.ShowDialog() == true) // Result could be true, false, or null
                this.FileName = d.FileName;
        }


        public static readonly DependencyProperty FileNameProperty = DependencyProperty.Register("FileName", typeof(string), typeof(FileInputBox));
        //***Создаем маршрутизируемое событие***
        public static readonly RoutedEvent FileNameChangedEvent = EventManager.RegisterRoutedEvent("FileNameChanged",
            RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(FileInputBox));


        //Только SetValue, GetValue 
        public string FileName
        {
            get { return (string)GetValue(FileNameProperty); }
            set { SetValue(FileNameProperty, value); }
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            e.Handled = true;
            //****************вызыв события***************************************
            RoutedEventArgs args = new RoutedEventArgs(FileNameChangedEvent);
            RaiseEvent(args);//наследуемое от UIElement
        }

        object Lock=null;//переменная для блокировки()

        //*******************************************************
        /*
        Контекстное ключевое слово add определяет метод доступа настраиваемого события, 
        который вызывается, когда клиентский код подписывается на событие. 
        Если указан настраиваемый метод доступа add, 
        также необходимо указать метод доступа remove.          
         */
        public event RoutedEventHandler FileNameChanged
        {
            
            add  {
                lock (Lock)//заблокировали доступ к событию
                { this.AddHandler(FileNameChangedEvent, value);//AddHandler из UIElement
                } }//разблокировали доступ
            remove 
            { lock (Lock)//заблокировали
                {
                    RemoveHandler(FileNameChangedEvent, value);
                }//разблокировали
            }
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            if (oldContent != null)
                throw new InvalidOperationException("You can't change Content!");
        }


    }
}
