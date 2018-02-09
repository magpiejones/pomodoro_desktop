using Pomodoro.MVVM;
using Pomodoro.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro.Pages.NewPomodoro
{
    public class Break : MVVM.DisposableViewModel
    {
        private readonly IUserInterface _ui;
        private readonly Timer _timer;

        private readonly DateTime _startedAt;
        private readonly TimeSpan _duration;

        public Break(MVVM.IUserInterface ui, ISettings settings)
        {
            _ui = ui;
            _startedAt = DateTime.UtcNow;
            _duration = settings.BreakDuration;

            _timer = new Timer(updateInterval: TimeSpan.FromSeconds(1.0 / 24), onUpdate: (TimeSpan elapsed) => _ui.Perform(() => Update(elapsed)));
            _timer.Start();
        }

        protected override void DisposeOfManagedResources()
        {
            _timer?.Dispose();
        }

        private void Update(TimeSpan elapsed)
        {
            var progress = (elapsed.TotalMilliseconds / _duration.TotalMilliseconds);
            if (progress >= 1.0)
            {
                _timer.Stop();
                _ui.TransitionToPage<Pomodoro>();
            }

            this.Progress = progress;
            NotifyPropertyChanged("Progress");

            this.ProgressDisplayText = elapsed.ToString();
            NotifyPropertyChanged("ProgressDisplayText");
        }

        public double Progress { get; private set; }
        public string ProgressDisplayText { get; private set; }
    }
}
