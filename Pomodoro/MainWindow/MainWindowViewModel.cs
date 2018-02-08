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
        private readonly Server.IDispatcher _server;
        private readonly MVVM.IUserInterface _ui;

        public MainWindowViewModel(Server.IDispatcher server, MVVM.IUserInterface ui)
        {
            _server = server;
            _ui = ui;

            JObject data = new JObject();
            {
                dynamic _data = data;
                _data.user = "4b3ca9d7-8d05-4fb6-b9af-57b3316deb37";
                dynamic pomodoro = new JObject();
                pomodoro.alias = "Emails";
                pomodoro.started = FormatDateTime(DateTime.Now);
                pomodoro["intended-duration"] = TimeSpan.FromMinutes(25).TotalSeconds.ToString();
                pomodoro["actual-duration"] = TimeSpan.FromMinutes(25).TotalSeconds.ToString();
                pomodoro.status = "COMPLETED";
                _data.pomodoro = pomodoro;
            }

            this.Update = new DelegateCommand(() =>
            {
                _ui.Perform(() =>
                {
                    this.Status = "Sent @" + DateTime.Now.TimeOfDay + "...";
                    base.NotifyPropertyChanged("Status");
                });

                _server.Post("/pomodoros/new", data,
                    (Task<string> previous) =>
                    {
                        if (previous.IsFaulted)
                        {
                            HandleError(previous);
                        }

                        var status = previous.Result;

                        _ui.Perform(() =>
                        {
                            this.Status = "Updated @" + DateTime.Now.TimeOfDay + ": " + status;
                            base.NotifyPropertyChanged("Status");
                        });
                    });
            });
        }


        private void HandleError(Task<string> failedTask)
        {
            var exception = failedTask.Exception is AggregateException ? failedTask.Exception.InnerException : failedTask.Exception;

            _ui.Perform(() =>
            {
                this.Status = "Exception: " + exception.Message;
                base.NotifyPropertyChanged("Status");
            });
        }

        private string FormatDateTime(DateTime data)
        {
            return String.Format("{0:dd/MM/yyyy HH:mm:ss}", data);
        }

        public MVVM.ICommand Update { get; private set; }
        public string Status { get; private set; }

        public MVVM.ViewModel CurrentContent { get { return _ui.CurrentPage; } }
    }
}
