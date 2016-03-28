using LogGrabber.Client;
using System;

namespace TestApp
{
    public class Program
    {
        class MyCustomException : Exception
        {
            public MyCustomException()
            {
            }

            public MyCustomException(string message)
                : base(message)
            {
            }

            public MyCustomException(string message, Exception innerException)
                : base(message, innerException)
            {
            }
        }

        static LogGrabberClient _client = new LogGrabberClient(new Uri("http://localhost:49614/api/logs"));

        public static void Main(string[] args)
        {
            try
            {
                throw new MyCustomException("Test error");
            }
            catch (MyCustomException ex)
            {
                _client.Send("Error detected", ex);
            }
            while (true)
            {
            }
        }
    }
}
