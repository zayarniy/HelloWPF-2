using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace OwnRealiseDependecyProperty
{
    public class MyButton: ButtonBase
    {
        public static readonly DependencyProperty OurDependencyProperty;

        static MyButton()
        {
           
            //Регистрируем свойство (OurDependencyProperty - имя произвольное)
            MyButton.OurDependencyProperty = DependencyProperty.Register("OurDependency", //имя зависимого свойства
                typeof(bool), //тип
                typeof(MyButton), //класс
                new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnOurDependencyPropertyChanged)));

        }

        //Обертка в виде обычного своства .NET(для того, чтобы иметь возможность просто обратиться
        //к свойству, необязательно)
        public bool OurDependency
        {
            get { return (bool)GetValue(MyButton.OurDependencyProperty); }
            set { SetValue(MyButton.OurDependencyProperty, value); }
        }

        //Метод, вызываемый при изменении свойства (необязательно)
        static void OnOurDependencyPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {

        }

        //snipped "propdp"
    }
}
