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

namespace COMBOBOX
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


        //private void ComboBoxItem_Drop(object sender, DragEventArgs e)
        //{
        //    MessageBox.Show(cbComboBox.SelectedItem + " " + cbComboBox.SelectedIndex);
        //}

        private void ComboBoxItem_Drop(object sender, EventArgs e)
        {

        }

        private void cbComboBox_DropDownOpened(object sender, EventArgs e)
        {
            MessageBox.Show(cbComboBox.SelectedItem + "  " + cbComboBox.SelectedIndex+"  "+cbComboBox.SelectedValue);
        }

    }
}

