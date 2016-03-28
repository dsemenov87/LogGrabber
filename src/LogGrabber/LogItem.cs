using System;

namespace LogGrabber
{
    public class LogItem
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime Occured { get; set; }
        public Error Error { get; set; }
        public Status Status { get; set; }
        public User User { get; set; }
        public Application Application { get; set; }
    }
}
