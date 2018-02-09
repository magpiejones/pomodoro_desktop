using Pomodoro.MVVM;
using Pomodoro.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro.Pages.NewPomodoro
{
    public class Pomodoro : DisposableViewModel
    {
        private bool _inProgress = false;

        public Pomodoro(IUserInterface ui, ISettings settings)
        {
            Begin = new DelegateCommand(
                canExecute: _ => !_inProgress,
                execute: _ =>
                {
                    _inProgress = true;
                    Begin.RaiseCanExecuteChanged();

                    Progress.Start();
                });

            Progress = new ProgressTimer(ui, settings.PomodoroDuration,
                onComplete: () =>
                {
                    _inProgress = false;
                    Begin.RaiseCanExecuteChanged();

                    ui.TransitionToPage<Break>();
                });
        }

        protected override void DisposeOfManagedResources()
        {
            Progress.Dispose();
        }

        public string Name { get; set; }
        public ICommand Begin { get; private set; }
        public ProgressTimer Progress { get; private set; }
    }
}
