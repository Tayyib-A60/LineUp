using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Core;
using API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace API.Persistence
{
    public class LineUpRepository : ILineUpRepository
    {
        private LineUpContext _context { get; }
        private IConfiguration _configuration { get; }
        public LineUpRepository (LineUpContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Entry(entity).State = EntityState.Added;
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }
        public async Task<bool> SaveAllChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> EntityExists<T>(T entityName) where T: class
        {
            if(entityName is Booking) {
                var booking = entityName as Booking;
                if(await _context.Bookings.AnyAsync(b => b.Id == booking.Id)) {
                    return true;
                }
                return false;
            } else if(entityName is Amenity) {
                var amenity = entityName as Amenity;
                if(await _context.Amenities.AnyAsync(a => a.Id == amenity.Id || a.Name.ToLower() == amenity.Name.ToLower())) {
                    return true;
                }
                return false;
            } else if(entityName is Enquiry) {
                var enquiry = entityName as Enquiry;
                if(await _context.Enquiries.AnyAsync(e => e.Id == enquiry.Id)) {
                    return true;
                }
                return false;
            } else if(entityName is Space) {
                var space = entityName as Space;
                if(await _context.Spaces.AnyAsync(s => s.Id == space.Id)) {
                    return true;
                }
                return false;
            } else if(entityName is SpaceType) {
                var spaceType = entityName as SpaceType;
                if(await _context.SpaceTypes.AnyAsync(st => st.Id == spaceType.Id || st.Type.ToLower() == spaceType.Type.ToLower())) {
                    return true;
                }
                return false;
            }
            return false;
        }
        public async Task<Enquiry> GetEnquiry(int enquiryId)
        {
            return await _context.Enquiries
                                .FirstOrDefaultAsync(e => e.Id == enquiryId);
        }
        public async Task<IEnumerable<Enquiry>> GetEnquiries(int userId)
        {
            return await _context.Enquiries
                                .Where(e => e.UserId == userId)
                                .ToListAsync();
        }
        public async Task<Space> GetSpace(int spaceId)
        {
            return await _context.Spaces
                            .Include(space => space.Type)
                            .Include(space => space.Amenities)
                            .FirstOrDefaultAsync(s => s.Id == spaceId);
        }
        public async Task<QueryResult<Space>> GetSpaces(SpaceQuery query)
        {
            var spaces =  _context.Spaces
                                    .Where(sp => sp.UserId == query.UserId)
                                    .Include(sp => sp.Type)
                                    // .Include(sp => sp.Photos.Where(p => p.IsMain == true))
                                    .AsQueryable();
            spaces = FilterSpaces(query, spaces);
            int count = spaces.Count();
            spaces = spaces.Skip((query.CurrentPage - 1) * query.PageSize)
                                    .Take(query.PageSize);
            var queryResult = new QueryResult<Space>();
            queryResult.TotalItems = count;
            queryResult.Items = await spaces.ToListAsync();
            return queryResult;
        }
        private IQueryable<Space> FilterSpaces(SpaceQuery query, IQueryable<Space> spaces)
        {
            if(query.Price > 0)
                spaces = spaces.Where(sp => sp.Price <= query.Price);
            if(!string.IsNullOrWhiteSpace(query.Location))
                spaces = spaces.Where(sp => sp.Location.ToLower().Contains(query.Location));
            if(!string.IsNullOrWhiteSpace(query.SearchString))
                spaces = spaces.Where(sp => sp.Location.ToLower().StartsWith(query.SearchString) || sp.Location.ToLower().Contains(query.SearchString) || sp.Name.StartsWith(query.SearchString) || sp.Name.ToLower().Contains(query.SearchString) || sp.Description.Contains(query.SearchString));
            if(query.Size > 0)
                spaces = spaces.Where(sp => Convert.ToInt64(sp.Size) <= query.Size);
            if(!string.IsNullOrWhiteSpace(query.SpaceType))
                spaces = spaces.Where(sp => sp.Type.ToString().ToLower() == query.SpaceType.ToLower());
            return spaces;            
        }
        public async Task<SpaceType> GetSpaceType(int spaceTypeId)
        {
            return await _context.SpaceTypes
                            .FirstOrDefaultAsync(st => st.Id == spaceTypeId);
        }
        public async Task<IEnumerable<SpaceType>> GetSpaceTypes()
        {
            return await _context.SpaceTypes.ToListAsync();
        }
        public async Task<Amenity> GetAmenity(int amenityId)
        {
            return await _context.Amenities
                            .FirstOrDefaultAsync(a => a.Id == amenityId);
        }
        public async Task<IEnumerable<Amenity>> GetAmenities()
        {
            return await _context.Amenities
                            .ToListAsync();
        }
        public async Task<Booking> GetBooking(int bookingId)
        {
            return await _context.Bookings
                            .FirstOrDefaultAsync(b => b.Id == bookingId);
        }
        public async Task<IEnumerable<Booking>> GetBookings(int userId)
        {
            return await _context.Bookings
                            .Where(b => b.UserId == userId)
                            .ToListAsync();
        }
    }
}