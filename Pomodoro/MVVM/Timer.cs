using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Pomodoro.MVVM
{
    sealed class Timer : IDisposable
    {
        private readonly System.Timers.Timer _timer;
        private DateTime _startedAt;

        public Timer(TimeSpan updateInterval, Action<TimeSpan> onUpdate)
        {
            _timer = new System.Timers.Timer(updateInterval.TotalMilliseconds);

            _timer.Elapsed += (sender, args) => onUpdate(DateTime.UtcNow - _startedAt);
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public void Start()
        {
            _startedAt = DateTime.UtcNow;
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
            _startedAt = default(DateTime);
        }
    }
}
