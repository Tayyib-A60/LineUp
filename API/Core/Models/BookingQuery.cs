using System;
using API.Extension;

namespace API.Core.Models
{
    public class BookingQuery: IQueryObject
    {
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }
        public int CurrentPage { get; set; }
        public string SearchString { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public DateTime TimeBooked { get; set; }
    }
}