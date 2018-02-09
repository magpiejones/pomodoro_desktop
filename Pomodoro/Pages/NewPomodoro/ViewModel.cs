using Pomodoro.MVVM;
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
        private readonly TimeSpan _duration = TimeSpan.FromSeconds(20);

        private System.Timers.Timer _timer;
        private DateTime _startedAt;

        public ViewModel(IUserInterface ui)
        {
            _ui = ui;

            _timer = new System.Timers.Timer(50);
            _timer.Elapsed += (sender, args) => _ui.Perform(Update);

            this.Begin = new DelegateCommand(
                _ => true,
                _ =>
                {
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
            var now = DateTime.Now;
            var actualDuration = now - _startedAt;
            var progress = (actualDuration.TotalMilliseconds / _duration.TotalMilliseconds);
            if (progress >= 1.0)
            {
                _timer.Stop();
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
