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
        public string Location { get; set; }   
        public string Description { get; set; }
        public int Size { get; set; }
        public SpaceType Type { get; set; }
        public double Price { get; set; }
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