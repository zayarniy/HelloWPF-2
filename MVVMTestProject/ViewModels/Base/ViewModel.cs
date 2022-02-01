using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MVVMTestProject.ViewModels.Base
{
    class ViewModel : INotifyPropertyChanged//, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));

        }

        //Проверка на заколеванность обновления свойств
        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(PropertyName);
            return true;
        }



        /* для реализации IDispose
        ~ViewModel()
        {
            Dispose(false);
        }



        private bool _Disposed;

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool Disposing)
        {
            if (!Disposing || _Disposed) return;
        }
        */
    }
}
