using System;
using System.ComponentModel;

//Поскольку в этом приложении мы имеем дело с несколькими классами модели представления, и у них есть общие функциональные возможности, мы начнем с определения базового класса:
namespace MVVMDemo
{
    abstract class BaseViewModel : INotifyPropertyChanged
    {
        private string _displayName="Unknown";

        public string DisplayName
        {
            get
            {
                return _displayName;
            }
            set
            {
                _displayName = value;
                OnPropertyChanged("DisplayName");
            }
        }

        protected BaseViewModel(string displayName)
        {
            this.DisplayName = displayName;
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
