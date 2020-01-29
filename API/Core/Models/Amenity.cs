namespace API.Core.Models
{
    public class Amenity {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public Space Space { get; set; }
        public int SpaceId { get; set; }
    }
}