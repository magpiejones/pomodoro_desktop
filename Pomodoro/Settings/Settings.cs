using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro.Settings
{
    class Settings : ISettings
    {
        public TimeSpan PomodoroDuration { get;  set; }
        public TimeSpan BreakDuration { get; set; }
    }
}
