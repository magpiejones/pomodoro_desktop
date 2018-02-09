using Ninject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Pomodoro.MVVM
{
    class UserInterface : IUserInterface, INotifyPropertyChanged
    {
        private readonly IKernel _kernel;
        private readonly Dispatcher _dispatcher;

        public UserInterface(IKernel kernel)
        {
            _kernel = kernel;
            _dispatcher = System.Windows.Application.Current.Dispatcher;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModel CurrentPage { get; private set; }

        public void TransitionToPage<T>() where T : ViewModel
        {
            (this.CurrentPage as IDisposable)?.Dispose();
            this.CurrentPage = _kernel.Get<T>();

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentPage"));
        }

        public void Perform(Action action)
        {
            _dispatcher.Invoke(action);
        }
    }
}
