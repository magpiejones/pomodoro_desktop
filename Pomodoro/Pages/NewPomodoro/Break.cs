using Pomodoro.MVVM;
using Pomodoro.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro.Pages.NewPomodoro
{
    public class Break : DisposableViewModel
    {
        public Break(IUserInterface ui, ISettings settings)
        {
            Progress = new ProgressTimer(ui, settings.BreakDuration, () => ui.TransitionToPage<Pomodoro>());
            Progress.Start();
        }

        protected override void DisposeOfManagedResources()
        {
            Progress.Dispose();
        }

        public ProgressTimer Progress { get; private set; }
    }
}
