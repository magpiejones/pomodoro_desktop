using Pomodoro.MVVM;
using Pomodoro.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro.Pages.NewPomodoro
{
    public class Pomodoro : MVVM.DisposableViewModel
    {
        private readonly IUserInterface _ui;
        private readonly Timer _timer;
        private readonly TimeSpan _duration;

        private bool _inProgress = false;
        private DateTime _startedAt;

        public Pomodoro(IUserInterface ui, ISettings settings)
        {
            _ui = ui;

            _timer = new Timer(updateInterval: TimeSpan.FromSeconds(1.0 / 24), onUpdate: (TimeSpan elapsed) => _ui.Perform(() => Update(elapsed)));

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

        private void Update(TimeSpan elapsed)
        {
            var progress = (elapsed.TotalMilliseconds / _duration.TotalMilliseconds);
            if (progress >= 1.0)
            {
                _timer.Stop();

                _inProgress = false;
                this.Begin.RaiseCanExecuteChanged();

                _ui.TransitionToPage<Break>();
            }

            this.Progress = progress;
            NotifyPropertyChanged("Progress");

            this.ProgressDisplayText = elapsed.ToString();
            NotifyPropertyChanged("ProgressDisplayText");
        }

        public string Name { get; set; }
        public ICommand Begin { get; private set; }

        public double Progress { get; private set; }
        public string ProgressDisplayText { get; private set; }
    }
}
