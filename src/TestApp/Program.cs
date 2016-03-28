using LogGrabber.Client;
using System;
using System.Threading;

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

        static LogGrabberClient _logGrabberClient
            = new LogGrabberClient(new Uri("http://localhost:49614/api/logs"), "Dmitry", "123");

        public static void Main(string[] args)
        {
            try
            {
                throw new MyCustomException("AAAAAAAAA!!!!!!");
            }
            catch (MyCustomException ex)
            {
                _logGrabberClient.Send("Error detected", ex);
            }
            Thread.Sleep(1000);
        }
    }
}
