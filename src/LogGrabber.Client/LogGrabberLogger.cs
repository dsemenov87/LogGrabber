using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace LogGrabber.Client
{
    public class LogGrabberClient
    {
        private readonly Uri _uri;
        public LogGrabberClient(Uri uri)
        {
            _uri = uri;
        }

        public async void Send(string message, Exception ex, Status status = Status.Critical)
        {
            if (ex == null) throw new ArgumentNullException("ex");

            var logItem = new LogItem
            {
                Status = status,
                Message = message,
                Application = new Application
                {
                    Name = Assembly.GetExecutingAssembly().GetName().Name
                },
                Error = new Error
                {
                    Name = ex.GetType().Name,
                    Message = ex.Message,
                    CallStack = ex.StackTrace
                },
                Occured = DateTime.Now
            };
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(logItem);
            var response = await httpClient.PostAsync(_uri.ToString(),
                new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json"));
        }
    }
}
