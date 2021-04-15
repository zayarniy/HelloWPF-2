using System;
using System.Windows;
using System.Windows.Controls;


namespace TabSwitcher1
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class TabSwitcherControl : UserControl
    {
        public TabSwitcherControl()
        {
            InitializeComponent();
            btnPrevious.Click += BtnPrevious_Click;
           
         
        }



        
        

        #region DependencyProperty
        public static readonly DependencyProperty PrevTextProperty = DependencyProperty.Register("PrevText", typeof(string), typeof(TabSwitcherControl),new PropertyMetadata("Prev"));


        public string PrevText
        {
            get
            {
                return (string)GetValue(PrevTextProperty);
            }
            set
            {
                SetValue(PrevTextProperty, value);
            }
        }



        #endregion



        #region RoutedEvents
        public static readonly RoutedEvent btnPrevClickEvent = EventManager.RegisterRoutedEvent("btnPreviousClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TabSwitcherControl));

        public static readonly RoutedEvent btnNextClickEvent = EventManager.RegisterRoutedEvent("btnNextClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TabSwitcherControl));



        #endregion

        public event RoutedEventHandler btnPreviousClick
        {
            add { AddHandler(btnPrevClickEvent, value); }
            remove { RemoveHandler(btnPrevClickEvent, value); }
        }






        private void BtnPrevious_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            RoutedEventArgs args = new RoutedEventArgs(btnPrevClickEvent);
            RaiseEvent(args);
        }


        protected override void OnContentChanged(object oldContent, object newContent)
        {
            if (oldContent != null)
                throw new InvalidOperationException("You can't change Content!");
        }

    }
}
