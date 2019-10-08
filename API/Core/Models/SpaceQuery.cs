using API.Extension;

namespace API.Core.Models
{
    public class SpaceQuery : IQueryObject
    {
        public int UserId { get; set; }
        public string? SearchString { get; set; }
        public string? SpaceType { get; set; }
        public string? Location { get; set; }
        public int? Size { get; set; }
        public double? Price { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int CurrentPage { get; set; }
        public byte PageSize { get; set; } 
        
    }
}