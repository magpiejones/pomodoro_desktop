using Main = Pomodoro.MainWindow;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Ninject;

namespace Pomodoro
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private StandardKernel _kernel;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            _kernel = new StandardKernel(new[]
            {
                new ApplicationModule()
            });

            _kernel.Get<MVVM.MainApplicationWindow>().Show();
        }
    }
}
