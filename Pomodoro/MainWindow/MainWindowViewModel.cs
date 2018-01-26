using Pomodoro.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pomodoro.MainWindow
{
    public class MainWindowViewModel : ViewModel
    {
        public MainWindowViewModel()
        {
            this.Update = new MVVM.DelegateCommand(() =>
            {
                this.Status = "Updated @" + DateTime.Now.TimeOfDay;
                base.NotifyPropertyChanged("Status");
            });
        }

        public ICommand Update { get; private set; }
        public string Status { get; private set; }
    }
}
