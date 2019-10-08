using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace API.Controllers.DTOs
{
    public class ChatDTO {
        public int Id { get; set; }
        public ICollection<ChatMessageDTO> ChatMessages { get; set; }
        public ChatDTO()
        {
            this.ChatMessages = new Collection<ChatMessageDTO>();
            
        }
    }
}