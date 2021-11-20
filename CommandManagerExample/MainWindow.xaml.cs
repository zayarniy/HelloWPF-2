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

namespace CommandManagerExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static RoutedCommand CustomRoutedCommand = new RoutedCommand();
        public MainWindow()
        {
            InitializeComponent();
            //CommandBinding customCommandBinding = new CommandBinding(
            //    CustomRoutedCommand, ExecutedCustomCommand, CanExecuteCustomCommand);

            //// attach CommandBinding to root window
            //this.CommandBindings.Add(customCommandBinding);
            //StackPanel CustomCommandStackPanel = new StackPanel();
            //Button CustomCommandButton = new Button();
            //CustomCommandStackPanel.Children.Add(CustomCommandButton);

            //CustomCommandButton.Command = CustomRoutedCommand;
        }

        private void ExecutedCustomCommand(object sender, ExecutedRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("ExecuteCustomCommand is called");
            MessageBox.Show("Custom Command Executed");
        }

        // CanExecuteRoutedEventHandler that only returns true if
        // the source is a control.
        private void CanExecuteCustomCommand(object sender, CanExecuteRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("CanExecuteCustomCommand is called");
            Control target = e.Source as Control;

            if (target != null)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        private void Button_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Button Executed is called");
        }

        private void Button_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
            System.Diagnostics.Debug.WriteLine("Button CanExecute is called");
        }
    }
}
