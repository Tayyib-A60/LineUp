using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace API.Core.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Space SpaceBooked { get; set; }
        public DateTime BookingTime { get; set; }
        public DateTime UsingFrom { get; set; }
        public DateTime UsingTill { get; set; }
        public BookingStatus Status { get; set; }
        public int BookedById { get; set; }
        public double TotalPrice { get; set; }
        public Chat Chat { get; set; }
    }
}