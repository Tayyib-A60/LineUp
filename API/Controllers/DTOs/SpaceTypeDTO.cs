using System.Collections.Generic;

namespace API.Controllers.DTOs
{
    public class SpaceTypeDTO
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public ICollection<SpaceDTO> Spaces { get; set; }
    }
}