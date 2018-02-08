using System;
using System.Collections.Generic;
using System.Text;

namespace SharedProject1
{
    public class LogMessage : EntryMessage
    {
        public string Message { get; set; }
        public LogMessage(string message, Guid identity) : base(identity)
        {
            Message = message;
        }
    }

    public abstract class EntryMessage
    {
        public string Path { get; set; }
        public Guid DeviceId { get; set; }

        public EntryMessage(Guid identity)
        {
            DeviceId = identity;
        }
    }
}
