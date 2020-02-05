using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace API.Core.Models
{
    public class Space
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Size { get; set; }
        public string MinimumTerm { get; set; }   
        public string LocationAddress { get; set; }
        public string Long { get; set; }
        public string  Lat { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public int TypeId { get; set; }
        public PricingOption SelectedPricingOption { get; set; }
        public DateTime DateCreated { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<Amenity> Amenities { get; set; }
        // public ICollection<Booking> Bookings { get; set; }
        public Space()
        {
            this.Amenities = new Collection<Amenity>();
            this.Photos = new Collection<Photo>();
            // this.Bookings = new Collection<Booking>();
        }
    }
}