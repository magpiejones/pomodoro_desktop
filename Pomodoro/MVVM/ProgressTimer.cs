using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro.MVVM
{
    public sealed class ProgressTimer : DisposableViewModel
    {
        private readonly TimeSpan _duration;
        private readonly Action _onComplete;
        private readonly Timer _timer;

        public ProgressTimer(IUserInterface ui, TimeSpan duration, Action onComplete)
        {
            _duration = duration;
            _onComplete = onComplete;

            _timer = new Timer(updateInterval: TimeSpan.FromSeconds(1.0 / 24), onUpdate: (TimeSpan elapsed) => ui.Perform(() => Update(elapsed)));
        }

        protected override void DisposeOfManagedResources()
        {
            _timer.Dispose();
        }

        public void Start()
        {
            _timer.Start();
        }

        private void Update(TimeSpan elapsed)
        {
            var progress = (elapsed.TotalMilliseconds / _duration.TotalMilliseconds);

            this.Progress = progress;
            NotifyPropertyChanged("Progress");

            this.ProgressDisplayText = elapsed.ToString();
            NotifyPropertyChanged("ProgressDisplayText");

            if (progress >= 1.0)
            {
                _timer.Stop();
                _onComplete();
            }
        }

        public double Progress { get; private set; }
        public string ProgressDisplayText { get; private set; }
    }
}
