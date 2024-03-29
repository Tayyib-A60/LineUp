using System.Collections.Generic;
using System.Collections.ObjectModel;
using API.Core.Models;

namespace API.Controllers.DTOs
{
    public class SpaceToUpdateDTO
    {
         public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public LocationDTO Location { get; set; }
        public PricingDTO PricePH { get; set; }
        public PricingDTO PricePD { get; set; }
        public PricingDTO PricePW { get; set; }  
        public string Description { get; set; }
        public string Size { get; set; }
        public SpaceType Type { get; set; }
        public SpaceToUpdateDTO()
        {
            
        }
    }
}