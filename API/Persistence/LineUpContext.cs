using API.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Persistence
{
    public class LineUpContext: DbContext
    {
        public LineUpContext(DbContextOptions<LineUpContext> options) : base(options) {}
        public DbSet<User> Users { get; set; }
        public DbSet<Enquiry> Enquiries { get; set; }
        public DbSet<Photo> Photos { get; set; } 
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Space> Spaces { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<SpaceType> SpaceTypes { get; set; }

         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}