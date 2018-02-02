using System;

namespace Pomodoro.MVVM
{
    public interface IUserInterface
    {
        void Perform(Action action);
    }
}