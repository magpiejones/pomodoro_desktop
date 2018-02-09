using Ninject.Modules;
using Pomodoro.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro
{
    class ApplicationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<Settings.ISettings>().ToConstant(new Settings.Settings
            {
                PomodoroDuration = TimeSpan.FromSeconds(20),
                BreakDuration = TimeSpan.FromSeconds(5)
            });

            Bind<MVVM.IUserInterface>().To<MVVM.UserInterface>();
            Bind<MVVM.MainApplicationWindow>().To<MainWindow.MainWindowView>();


            Bind<IDispatcher>().To<Dispatcher>().InSingletonScope();
        }
    }
}
