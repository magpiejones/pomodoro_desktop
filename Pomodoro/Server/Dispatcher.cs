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
    class Dispatcher : IDisposable
    {
        private HttpClient _client;
        private readonly Uri _uri = new Uri("https://requestb.in/thygqyth");

        public Dispatcher()
        {
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


        public void Get(Action<Task<string>> continuation)
        {
            var task = new Task<string>(() => ProcessResponse(_client.GetAsync(_uri).Result));
            task.ContinueWith(continuation);
            task.Start();
        }

        public void Post(JObject data, Action<Task<string>> continuation)
        {
            var content = GetContent(data);

            var task = new Task<string>(() => ProcessResponse(_client.PostAsync(_uri, content).Result));
            task.ContinueWith(continuation);
            task.Start();
        }

        public void Put(JObject data, Action<Task<string>> continuation)
        {
            var content = GetContent(data);

            var task = new Task<string>(() => ProcessResponse(_client.PutAsync(_uri, content).Result));
            task.ContinueWith(continuation);
            task.Start();
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
