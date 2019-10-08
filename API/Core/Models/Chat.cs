using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace API.Core.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public ICollection<ChatMessage> ChatMessages { get; set; }
        public Chat()
        {
            this.ChatMessages = new Collection<ChatMessage>();
            
        }
    }
}