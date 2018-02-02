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
            Bind<MVVM.MainApplicationWindow>().To<MainWindow.MainWindowView>();


            Bind<IDispatcher>().To<Dispatcher>().InSingletonScope();
        }
    }
}
