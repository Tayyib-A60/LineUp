using System;

namespace API.Controllers.DTOs
{
    public class EnquiryDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SpaceId { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
    }
}