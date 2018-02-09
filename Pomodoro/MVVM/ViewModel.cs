using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro.MVVM
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            var @event = PropertyChanged;
            if (@event != null)
            {
                @event(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public abstract class DisposableViewModel : ViewModel, IDisposable
    {
        public void Dispose()
        {
            DisposeOfManagedResources();
        }

        protected abstract void DisposeOfManagedResources();
    }
}
