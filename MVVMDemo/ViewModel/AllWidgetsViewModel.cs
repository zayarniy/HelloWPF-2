using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace MVVMDemo
{
    
    class AllWidgetsViewModel : BaseViewModel
    {
        private WidgetRepository _widgets;
        //this collection of view models is available to the view to display however it wants
        public ObservableCollection<WidgetViewModel> WidgetViewModels { get; private set; }
                
        public AllWidgetsViewModel(WidgetRepository widgets)
            :base("All Widgets")
        {
            _widgets = widgets;
            _widgets.WidgetAdded += new EventHandler<EventArgs>(_widgets_WidgetAdded);

            CreateViewModels();
        }

        void _widgets_WidgetAdded(object sender, EventArgs e)
        {
            //каждый раз при вызове добавление виджетов вызываем CreateViewModels
            CreateViewModels();
        }

        //создаем модель представления
        private void CreateViewModels()
        {
            //создаем список с виджетами
            WidgetViewModels = new ObservableCollection<WidgetViewModel>();
            //добавляем каждый виджет из модели в модель представления
            foreach (Widget w in _widgets.Widgets)
            {
                WidgetViewModels.Add(new WidgetViewModel(w));
            }
            //сообщаем имся свойства, которое изменилось
            OnPropertyChanged("WidgetViewModels");
        }
    }
}
