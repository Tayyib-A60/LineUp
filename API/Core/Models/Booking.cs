using System;

namespace API.Core.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string BookingRef { get; set; }
        public int UserId { get; set; }
        public int IdOfSpaceBooked { get; set; }
        public int NoOfGuests { get; set; }
        public int BookedById { get; set; }
        public double TotalPrice { get; set; }
        public string AmenitiesSelected { get; set; }
        public bool CreatedByOwner { get; set; }
        public DateTime BookingTime { get; set; }
        public DateTime UsingFrom { get; set; }
        public DateTime UsingTill { get; set; }
        public BookingStatus Status { get; set; }
        public string BookedForName { get; set; }
        public string BookedForEmail { get; set; }
        public string BookedForPhone { get; set; }
        public Chat Chat { get; set; }
    }
}