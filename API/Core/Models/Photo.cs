using System;

namespace API.Core.Models
{
    public class Photo
    {
        public int Id { get; set; } 
        public string FileName { get; set; }
        public bool IsMain { get; set; }
        public DateTime DateCreated { get; set; }
        public string PublicId { get; set; }
        public Space Space { get; set; }
        public int SpaceId { get; set; }
    }
}