namespace API.Core.Models
{
    public class MerchantMetrics
    {
        public TopSpaceUsage TopSpaceHoursUsed { get; set; }
        public TopSpaceUsage TopSpaceRevenueUsage { get; set; }
        public long TotalSpaceBooking { get; set; }
        public long TotalSpaceReservation { get; set; }
        public double TotalRevenue { get; set; }
    }
}