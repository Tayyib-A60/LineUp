using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace API.Core.Models
{
    public class Space
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }   
        public string Description { get; set; }
        public string Size { get; set; }
        public SpaceType Type { get; set; }
        public Pricing PricePH { get; set; }
        public Pricing PricePD { get; set; }
        public Pricing PricePW { get; set; }
        public DateTime DateCreated { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<Amenity> Amenities { get; set; }
        public Space()
        {
            this.Amenities = new Collection<Amenity>();
            this.Photos = new Collection<Photo>();
        }
    }
}