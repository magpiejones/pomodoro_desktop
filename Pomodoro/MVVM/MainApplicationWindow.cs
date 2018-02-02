using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pomodoro.MVVM
{
    public abstract class MainApplicationWindow : Window
    {
        public MainApplicationWindow()
        {
            Application.Current.MainWindow = this;
        }
    }
}
