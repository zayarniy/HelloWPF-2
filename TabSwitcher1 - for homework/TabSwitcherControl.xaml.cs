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



        #region properties
        private bool bHidebtnPrevious = false; // поле, соответствующее тому, будет ли скрыта кнопка «Предыдущий»
        private bool bHideBtnNext = false; // поле, соответствующее тому, будет ли скрыта кнопка «Следующий»
        /// <summary>
        /// Свойство, соответствующее тому, будет ли скрыта кнопка «Предыдущий». 
        /// Чтобы Свойство отразилось на PropertiesGrid у нашего контрола, его нужно представить именно свойством, а не полем
        /// </summary>
        public bool IsHidebtnPrevious
        {
            get { return bHidebtnPrevious; }
            set
            {
                bHidebtnPrevious = value;
                SetButtons(); // метод, который отвечает на отрисовку кнопок
            }
        }// IsHidebtnPrevious
        public bool IsHideBtnNext
        {
            get { return bHideBtnNext; }
            set
            {
                bHideBtnNext = value;
                SetButtons(); // метод, который отвечает за отрисовку кнопок
            }
        }// IsHidebtnPrevious
        private void BtnNextTruebtnPreviousFalse()
        {
            btnNext.Visibility = Visibility.Hidden;
            btnPrevious.Visibility = Visibility.Visible;
            btnPrevious.Width = 229;
            btnNext.Width = 0;
            btnPrevious.HorizontalAlignment = HorizontalAlignment.Stretch;
        }
        private void btnPreviousTrueBtnNextFalse()
        {
            btnPrevious.Visibility = Visibility.Hidden;
            btnNext.Visibility = Visibility.Visible;
            btnNext.Width = 229;
            btnPrevious.Width = 0;
            btnNext.HorizontalAlignment = HorizontalAlignment.Stretch;
        }
        private void btnPreviousFalseBtnNextFalse()
        {
            btnNext.Visibility = Visibility.Visible;
            btnPrevious.Visibility = Visibility.Visible;
            btnNext.Width = 115;
            btnPrevious.Width = 114;
        }
        private void btnPreviousTrueBtnNextTrue()
        {
            btnPrevious.Visibility = Visibility.Hidden;
            btnNext.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// Метод, который отвечает за отрисовку кнопок.
        /// Есть три варианта: когда обе кнопки доступны; доступна одна и недоступна вторая; обе кнопки недоступны
        /// </summary>
        private void SetButtons()
        {
            if (bHidebtnPrevious && bHideBtnNext) btnPreviousTrueBtnNextTrue();
            else if (!bHideBtnNext && !bHidebtnPrevious) btnPreviousFalseBtnNextFalse();
            else if (bHideBtnNext && !bHidebtnPrevious) BtnNextTruebtnPreviousFalse();
            else if (!bHideBtnNext && bHidebtnPrevious) btnPreviousTrueBtnNextFalse();
        }
        #endregion

        
        

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
