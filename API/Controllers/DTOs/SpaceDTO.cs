using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using API.Core.Models;

namespace API.Controllers.DTOs
{
    public class SpaceDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }   
        public string Description { get; set; }
        public string Size { get; set; }
        public SpaceType Type { get; set; }
        public double Price { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<Amenity> Amenities { get; set; }
        public SpaceDTO()
        {
            this.Amenities = new Collection<Amenity>();
            this.Photos = new Collection<Photo>();
        }
    }
}