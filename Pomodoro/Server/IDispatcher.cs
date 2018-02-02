using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace Pomodoro.Server
{
    public interface IDispatcher
    {
        Uri RootUri { get; }

        void Get(string route, Action<Task<string>> continuation);
        void Post(string route, JObject data, Action<Task<string>> continuation);
        void Put(string route, JObject data, Action<Task<string>> continuation);
    }
}