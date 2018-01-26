using Pomodoro.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Dynamic;
using Newtonsoft.Json.Linq;

namespace Pomodoro.MainWindow
{
    public class MainWindowViewModel : ViewModel
    {
        public MainWindowViewModel()
        {
            var ui = new MVVM.UserInterface();

            JObject data = new JObject();
            {
                dynamic _data = data;
                _data.user = "4b3ca9d7-8d05-4fb6-b9af-57b3316deb37";
                dynamic pomodoro = new JObject();
                pomodoro.alias = "Emails";
                pomodoro.started = DateTime.Now;
                pomodoro.ended = DateTime.Now + TimeSpan.FromMinutes(25);
                pomodoro.intended_duration_minutes = 25;
                pomodoro.status = "COMPLETED";
                _data.pomodoro = pomodoro;
            }

            this.Update = new DelegateCommand(() =>
            {
                ui.Perform(() =>
                {
                    this.Status = "Sent @" + DateTime.Now.TimeOfDay + "...";
                    base.NotifyPropertyChanged("Status");
                });

                try
                {
                    using (var server = new Server.Dispatcher())
                    {
                        server.Post(data,
                            (Task<string> previous) =>
                            {
                                var status = previous.Result;

                                ui.Perform(() =>
                                {
                                    this.Status = "Updated @" + DateTime.Now.TimeOfDay + ": " + status;
                                    base.NotifyPropertyChanged("Status");
                                });
                            });
                    }
                }
                catch(Exception e)
                {
                    ui.Perform(() =>
                    {
                        this.Status = "Exception: " + e.Message;
                        base.NotifyPropertyChanged("Status");
                    });
                }
            });
        }

        public ICommand Update { get; private set; }
        public string Status { get; private set; }
    }
}
