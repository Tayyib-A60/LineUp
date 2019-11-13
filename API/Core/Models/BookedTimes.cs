using System;

namespace API.Core.Models
{
    public class BookedTimes
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Status { get; set; }
    }
}