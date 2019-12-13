using System;

namespace API.Core.Models
{
    public class Photo
    {
        public int Id { get; set; } 
        public string FileName { get; set; }
        // public int UserId { get; set; }
        public int SpaceId { get; set; }
        public bool IsMain { get; set; }
        public DateTime DateCreated { get; set; }

        public string PublicId { get; set; }
    }
}