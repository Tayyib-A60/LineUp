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
    }
}