using API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace API.Persistence
{
    public class LineUpContext: DbContext
    {
        public LineUpContext(DbContextOptions<LineUpContext> options) : base(options) {}
        public DbSet<User> Users { get; set; }
        // public DbSet<Enquiry> Enquiries { get; set; }
        public DbSet<Photo> Photos { get; set; } 
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Space> Spaces { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<SpaceType> SpaceTypes { get; set; }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        //     optionsBuilder.UseMySql("Server=us-cdbr-iron-east-04.cleardb.net;Database=heroku_73d1adb9191c13b;Uid=b3e71c2faf8026;Pwd=c367c02e");
        // }
         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<User>()
            //             .HasMany(u => u.Spaces)
            //             .WithOne(s => s.User)
            //             .OnDelete(DeleteBehavior.Restrict);

            // modelBuilder.Entity<Space>()
            //             .HasOne(s => s.User)
            //             .WithMany(u => u.Spaces)
            //             .OnDelete(DeleteBehavior.Restrict);

            // modelBuilder.Entity<Booking>()
        }
    }
}