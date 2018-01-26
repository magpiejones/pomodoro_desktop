using Main = Pomodoro.MainWindow;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Pomodoro
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var viewModel = new Main.MainWindowViewModel();

            var view = new Main.MainWindowView(viewModel);
            base.MainWindow = view;
            view.Show();
        }
    }
}
