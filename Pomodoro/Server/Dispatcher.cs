using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro.Server
{
    class Dispatcher : IDisposable, IDispatcher
    {
        private HttpClient _client;

        public Dispatcher()
        {
            this.RootUri = new Uri("http://127.0.0.1:5000/api/v1/user/4b3ca9d7-8d05-4fb6-b9af-57b3316deb37");

            _client = new HttpClient();
            _client.Timeout = new TimeSpan(0, 0, 10);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //todo: add user id to the headers?
        }

        public void Dispose()
        {
            if (_client != null)
            {
                _client.Dispose();
                _client = null;
            }
        }


        public Uri RootUri { get; private set; }

        public void Get(string route, Action<Task<string>> continuation)
        {
            var task = new Task<string>(() => ProcessResponse(_client.GetAsync(GetUri(route)).Result));
            task.ContinueWith(continuation);
            task.Start();
        }

        public void Post(string route, JObject data, Action<Task<string>> continuation)
        {
            var content = GetContent(data);

            var task = new Task<string>(() => ProcessResponse(_client.PostAsync(GetUri(route), content).Result));
            task.ContinueWith(continuation);
            task.Start();
        }

        public void Put(string route, JObject data, Action<Task<string>> continuation)
        {
            var content = GetContent(data);

            var task = new Task<string>(() => ProcessResponse(_client.PutAsync(GetUri(route), content).Result));
            task.ContinueWith(continuation);
            task.Start();
        }

        private Uri GetUri(string route)
        {
            return new Uri(String.Join("/", RootUri.ToString(), route));
        }


        private HttpContent GetContent(JObject data)
        {
            var json = JsonConvert.SerializeObject(data);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private string ProcessResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
    }
}
