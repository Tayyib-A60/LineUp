namespace API.Controllers.DTOs
{
    public class BookingQueryDTO
    {
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }
        public int CurrentPage { get; set; }
    }
}