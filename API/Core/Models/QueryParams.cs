using API.Extension;

namespace API.Core.Models
{
    public class QueryParams
    {
        public string Month { get; set; }
        public string Year { get; set; }
        public bool IsSortAscending { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
    }
}