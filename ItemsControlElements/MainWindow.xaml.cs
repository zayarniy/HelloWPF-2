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

namespace ItemsControlElements
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<int> list = new List<int>();
            for (int i = 0; i < 10000; i++)
                list.Add(i); 

            lbList.ItemsSource = list;
            //Добавление WrapPanel с ListBox с помощью процедурного языка
            //FrameworkElementFactory panelFactory = new FrameworkElementFactory(typeof(WrapPanel)); myListBox.ItemsPanel = new ItemsPanelTemplate(panelFactory)
        }
    }
}
