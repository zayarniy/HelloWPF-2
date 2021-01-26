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

namespace LogicTree
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Console.WriteLine("*************Logical Tree******************");
            PrintLogicalTree(0, this);

        }

        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            Console.WriteLine("*************Visual Tree******************");
            PrintVisualTree(0, this);
        }

        void PrintLogicalTree(int depth, object obj)
        {
            // Напечатать объект с предшествующими пробелами,
            // число которых соответствует глубине вложенности
            Console.WriteLine(new string(' ', depth) + obj);
            // Иногда листовые узлы не принадлежат классу // DependencyObject (например, строки)
            if (!(obj is DependencyObject)) return;
            // Рекурсивный вызов для каждого логического // дочернего узла
            foreach (object child in LogicalTreeHelper.GetChildren(obj as DependencyObject))
                PrintLogicalTree(depth + 1, child);
        }

        void PrintVisualTree(int depth, DependencyObject obj)
        {
            // Напечатать объект с предшествующими пробелами,
            // число которых соответствует глубине вложенности
            Console.WriteLine(new string(' ', depth) + obj);
            // Рекурсивный вызов для каждого визуального // дочернего узла
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++) PrintVisualTree(depth + 1, VisualTreeHelper.GetChild(obj, i));
        }
    }
}
