using System;
using API.Core.Models;

namespace API.Controllers.DTOs
{
    public class CustomerBookingsToReturn
    {
        public int Id { get; set; }
        public int NoOfGuests { get; set; }
        public int BookedById { get; set; }
        public int IdOfSpaceBooked { get; set; }
        public DateTime BookingTime { get; set; }
        public DateTime UsingFrom { get; set; }
        public DateTime UsingTill { get; set; }
        public BookingStatus Status { get; set; }
        public double TotalPrice { get; set; }
        public string SpaceLocation { get; set; }
        public string SpaceName { get; set; }
        // public Space SpaceBooked { get; set; }
    }
}