using System;

namespace Pomodoro.MVVM
{
    public interface IUserInterface
    {
        ViewModel CurrentPage { get; }
        void TransitionToPage<T>() where T : ViewModel;

        void Perform(Action action);
    }
}