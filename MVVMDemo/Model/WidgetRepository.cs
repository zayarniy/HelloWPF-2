using System;
using System.Collections.Generic;

namespace MVVMDemo
{
    class WidgetRepository
    {
        public event EventHandler<EventArgs> WidgetAdded;

        protected void OnWidgetAdded()
        {
            WidgetAdded?.Invoke(this, EventArgs.Empty);
        }
        private List<Widget> _widgets = new List<Widget>();

        public ICollection<Widget> Widgets
        {
            get
            {
                return _widgets;
            }
        }

        public Widget this[int index]
        {
            get
            {
                return _widgets[index];
            }
        }

        public WidgetRepository()
        {
            CreateDefaultWidgets();
        }

        public void AddWidget(Widget widget)
        {
            _widgets.Add(widget);
            OnWidgetAdded();//Сообщаем о добавлении элемента в список
        }

        //заполняем базу данных
        private void CreateDefaultWidgets()
        {
            AddWidget(new Widget(1, "Awesome widget", WidgetType.TypeA));
            AddWidget(new Widget(2, "Okay widget", WidgetType.TypeA));
            AddWidget(new Widget(3, "So-so widget", WidgetType.TypeB));
            AddWidget(new Widget(4, "Horrible widget", WidgetType.TypeB));
        }
    }
}
