using System;

namespace API.Core.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string By { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }

    }
}