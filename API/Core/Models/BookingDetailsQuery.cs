using System;
using System.Collections.Generic;
using API.Extension;

namespace API.Core.Models
{
    public class BookingDetailsQuery
    {
        public string SearchString { get; set; }
        public List<DateTime> DateStart { get; set; }
        public List<DateTime> DateEnd { get; set; }
        public DateTime TimeBooked { get; set; }
    }
}