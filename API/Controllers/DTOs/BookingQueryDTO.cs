using System;
using API.Core.Models;

namespace API.Controllers.DTOs
{
    public class BookingQueryDTO
    {
       public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public BookingStatus Status { get; set; }
        public byte PageSize { get; set; }
        public int CurrentPage { get; set; }
        public string SearchString { get; set; }
        public string DateStart { get; set; }
        public string DateEnd { get; set; }
    }
}