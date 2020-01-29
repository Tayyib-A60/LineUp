using API.Core.Models;

namespace API.Controllers.DTOs
{
    public class SpaceToCreate
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Size { get; set; }
        public string MinimumTerm { get; set; }
        public SpaceType Type { get; set; }
       
    }
}