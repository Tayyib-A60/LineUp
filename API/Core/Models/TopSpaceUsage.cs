namespace API.Core.Models
{
    public class TopSpaceUsage 
    {
        public int SpaceId { get; set; }
        public double TotalUsageInHours { get; set; }
        public double TopSpaceRevenue { get; set; }
    }
}