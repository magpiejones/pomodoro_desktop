using Pomodoro.MVVM;
using Pomodoro.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro.Pages.NewPomodoro
{
    public class Break : MVVM.ViewModel
    {
        private readonly IUserInterface _ui;
        private readonly System.Timers.Timer _timer;

        private readonly DateTime _startedAt;
        private readonly TimeSpan _duration;

        public Break(MVVM.IUserInterface ui, ISettings settings)
        {
            _ui = ui;
            _startedAt = DateTime.UtcNow;
            _duration = settings.BreakDuration;

            _timer = new System.Timers.Timer(interval: TimeSpan.FromSeconds(1.0 / 24).TotalMilliseconds);
            _timer.Elapsed += (sender, args) => _ui.Perform(Update);
            _timer.Start();
        }

        private void Update()
        {
            var now = DateTime.UtcNow;
            var actualDuration = now - _startedAt;
            var progress = (actualDuration.TotalMilliseconds / _duration.TotalMilliseconds);
            if (progress >= 1.0)
            {
                _timer.Stop();
                _ui.TransitionToPage<Pomodoro>();
            }

            this.Progress = progress;
            NotifyPropertyChanged("Progress");

            this.ProgressDisplayText = actualDuration.ToString();
            NotifyPropertyChanged("ProgressDisplayText");
        }

        public double Progress { get; private set; }
        public string ProgressDisplayText { get; private set; }
    }
}
