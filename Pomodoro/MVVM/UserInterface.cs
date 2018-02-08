using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Pomodoro.MVVM
{
    class UserInterface : IUserInterface
    {
        private readonly Dispatcher _dispatcher;

        public UserInterface()
        {
            _dispatcher = System.Windows.Application.Current.Dispatcher;
        }

        public void Perform(Action action)
        {
            _dispatcher.Invoke(action);
        }
    }
}
