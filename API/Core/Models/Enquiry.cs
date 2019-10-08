using System;
using System.ComponentModel.DataAnnotations;

namespace API.Core.Models
{
    public class Enquiry
    {
        public int Id { get; set; }
        public int UserId{ get; set; }
        public int SpaceId { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
    }
}