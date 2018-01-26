using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pomodoro.MVVM
{
    class DelegateCommand : ICommand
    {
        private static readonly Predicate<object> AlwaysTrue = parameter => true;

        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;

        public DelegateCommand(Predicate<object> canExecute, Action<object> execute)
        {
            if (canExecute == null)
            {
                throw new ArgumentNullException("canExecute");
            }
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _canExecute = canExecute;
            _execute = execute;
        }

        public DelegateCommand(Action<object> execute) : this(AlwaysTrue, execute)
        {// empty
        }

        public DelegateCommand(Action execute) : this(AlwaysTrue, parameter => execute())
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }
        }


        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        protected void RaiseCanExecuteChanged()
        {
            var @event = CanExecuteChanged;
            if (@event != null)
            {
                @event(this, EventArgs.Empty);
            }
        }
    }
}
