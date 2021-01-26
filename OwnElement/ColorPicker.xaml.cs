//https://professorweb.ru/my/WPF/Template/level18/18_2.php
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OwnElement
{
    /// <summary>
    /// Interaction logic for ColorPicker.xaml
    /// </summary>
    public partial class ColorPicker : UserControl
    {
        //Свойства зависимости
        //По принятому соглашению все поля типа DependencyProperty открытые,
        //статические и имеют имя, оканчивающееся словом Property.Некоторые компоненты
        //инфраструктуры требуют обязательного соблюдения этого соглашения,
        //например средства локализации, загрузчики XAML и пр.
        public static DependencyProperty ColorProperty;
        public static DependencyProperty RedProperty;
        public static DependencyProperty GreenProperty;
        public static DependencyProperty BlueProperty;

        //снипет propdp 

        static  ColorPicker()
        {
            //Регистрация свойств зависимости
            ColorProperty=DependencyProperty.Register
                (
                "Color",//Имя свойства
                typeof(Color),//Тип свойства
                typeof(ColorPicker),//Тип класса владельца
                new FrameworkPropertyMetadata(Colors.Black, //начальное значение
                new PropertyChangedCallback(OnColorChanged))//процедура обратного вызова
                );
            RedProperty = DependencyProperty.Register("Red", typeof(byte), typeof(ColorPicker), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChanged)));

            GreenProperty = DependencyProperty.Register("Green", typeof(byte), typeof(ColorPicker), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChanged)));

            BlueProperty = DependencyProperty.Register("Blue", typeof(byte), typeof(ColorPicker), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChanged)));

            //Регистрация маршрутизируемого события
            ColorChangedEvent = EventManager.RegisterRoutedEvent("ColorChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<Color>), typeof(ColorPicker));

        }

        public ColorPicker()
        {
            InitializeComponent();

        }

        public Color Color
        {
            get
            {
                return (Color)GetValue(ColorProperty);
            }
            set
            {
                DependencyProperty oldValue = ColorProperty;
                SetValue(ColorProperty, value);

            }
        }

        public byte Red
        {
            get { return (byte)GetValue(RedProperty); }
            set { SetValue(RedProperty, value); }
        }

        public byte Green
        {
            get { return (byte)GetValue(GreenProperty); }
            set { SetValue(GreenProperty, value); }
        }

        public byte Blue
        {
            get { return (byte)GetValue(BlueProperty); }
            set { SetValue(BlueProperty, value);                 
            }
        }

        private static void OnColorRGBChanged(DependencyObject
             sender, DependencyPropertyChangedEventArgs e)
        {
            ColorPicker colorPicker = (ColorPicker)sender;
            Color color = colorPicker.Color;
            if (e.Property == RedProperty) color.R = (byte)e.NewValue;
            else if (e.Property == GreenProperty)
                color.G = (byte)e.NewValue;
            else if (e.Property == BlueProperty)
                color.B = (byte)e.NewValue;
            colorPicker.Color = color;            
        }

        private static void OnColorChanged(DependencyObject sender,DependencyPropertyChangedEventArgs e)
        {
            Color newColor = (Color)e.NewValue;
            ColorPicker colorPicker = (ColorPicker)sender;
            colorPicker.Red = newColor.R;
            colorPicker.Green = newColor.G;
            colorPicker.Blue = newColor.B;
        }

        public static readonly RoutedEvent ColorChangedEvent;

        public event RoutedPropertyChangedEventHandler<Color> ColorChanged
        {
            add { AddHandler(ColorChangedEvent, value); }
            remove { RemoveHandler(ColorChangedEvent, value); }
        }

        public override string ToString()
        {
            return Red + ":" + Green + ":" + Blue;
        }

    }
}
