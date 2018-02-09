using Pomodoro.MVVM;
using Pomodoro.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro.Pages.NewPomodoro
{
    public class ViewModel : MVVM.DisposableViewModel
    {
        private readonly IUserInterface _ui;
        private readonly System.Timers.Timer _timer;
        private readonly TimeSpan _duration;

        private bool _inProgress = false;
        private DateTime _startedAt;

        public ViewModel(IUserInterface ui, ISettings settings)
        {
            _ui = ui;

            _timer = new System.Timers.Timer(interval: TimeSpan.FromSeconds(1.0 / 24).TotalMilliseconds);
            _timer.Elapsed += (sender, args) => _ui.Perform(Update);

            _duration = settings.PomodoroDuration;

            this.Begin = new DelegateCommand(
                _ => !_inProgress,
                _ =>
                {
                    _inProgress = true;
                    this.Begin.RaiseCanExecuteChanged();

                    _startedAt = DateTime.UtcNow;
                    _timer.Start();
                });
        }

        protected override void DisposeOfManagedResources()
        {
            _timer?.Dispose();
        }

        private void Update()
        {
            var now = DateTime.UtcNow;
            var actualDuration = now - _startedAt;
            var progress = (actualDuration.TotalMilliseconds / _duration.TotalMilliseconds);
            if (progress >= 1.0)
            {
                _timer.Stop();

                _inProgress = false;
                this.Begin.RaiseCanExecuteChanged();
            }

            this.Progress = progress;
            NotifyPropertyChanged("Progress");

            this.ProgressDisplayText = Math.Floor(progress * 100);
            NotifyPropertyChanged("ProgressDisplayText");
        }

        public string Name { get; set; }
        public ICommand Begin { get; private set; }

        public double Progress { get; private set; }
        public double ProgressDisplayText { get; private set; }
    }
}
