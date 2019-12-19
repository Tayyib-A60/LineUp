namespace API.Core.Models
{
    public class MerchantMetrics
    {
        public long BookingCount { get; set; } 
        public long ReservationCount { get; set; }
        public long EnquiryCount { get; set; }
        public long Revenue { get; set; }
        public long TopSpaceUsage { get; set; }
        public long LowSpaceUsage { get; set; }
    }
}