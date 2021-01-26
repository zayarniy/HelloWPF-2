using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
//Needs for InvokeProvider iip;
using System.Windows.Automation.Provider;//Needs refererence to UIAutomationProvider.dll
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BUTTON
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void btnHover_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Hover:"+DateTime.Now);
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("OK:"+DateTime.Now);
        }

        private void btnOtherButton_Click(object sender, RoutedEventArgs e)
        {
            ButtonAutomationPeer bap = new ButtonAutomationPeer(btnOK);
            IInvokeProvider iip = bap.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
            iip.Invoke();//Нажатие кнопки

        }

        private void rbtnRepeat_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Repeat button:" + DateTime.Now);
        }

        private void tbtnToggleButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Toggle button:" + DateTime.Now+"Checked:"+tbtnToggleButton.IsChecked);
        }
    }
}
