using System.Collections.Generic;
using System.Collections.ObjectModel;
using API.Core.Models;

namespace API.Controllers.DTOs
{
    public class SpaceToEdit {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public LocationDTO Location { get; set; }
        public PricingDTO PricePH { get; set; }
        public PricingDTO PricePD { get; set; }
        public PricingDTO PricePW { get; set; }  
        public string SelectedPricingOption { get; set; }
        public string MinimumTerm { get; set; } 
        public string Description { get; set; }
        public string Size { get; set; }
        public SpaceType Type { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<Amenity> Amenities { get; set; }
        public SpaceToEdit()
        {
            this.Amenities = new Collection<Amenity>();
            this.Photos = new Collection<Photo>();
        }
    }
}