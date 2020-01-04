using System;
using System.Collections.Generic;
using API.Core.Models;

namespace API.Controllers.DTOs
{
    public class BookingFromClientDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public SpaceDTO SpaceBooked { get; set; }
        public DateTime BookingTime { get; set; }
        public List<UsingTime> UsingTimes { get; set; }
        public BookingStatus Status { get; set; }
        public int BookedById { get; set; }
        public double TotalPrice { get; set; }
        public ChatDTO Chat { get; set; }
    }
}