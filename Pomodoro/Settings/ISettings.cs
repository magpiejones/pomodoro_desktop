using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro.Settings
{
    public interface ISettings
    {
        TimeSpan PomodoroDuration { get; }
        TimeSpan BreakDuration { get; }
    }
}
