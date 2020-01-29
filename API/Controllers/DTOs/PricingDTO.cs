namespace API.Controllers.DTOs
{
    public class PricingDTO
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public int SpaceId { get; set; }
    }
}