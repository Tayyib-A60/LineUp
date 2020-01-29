using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Core;
using API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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
        public void UpdateSpace(Space space) {
            _context.Spaces.Update(space);
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
            // else if(entityName is PricingOption) {
            //     var pricingOption = entityName as PricingOption;
            //     if(await _context.PricingOptions.AnyAsync(po => po.Id == pricingOption.Id || po.Option.ToLower() == pricingOption.Option.ToLower())) {
            //         return true;
            //     }
            //     return false;
            // }
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
                            .Include(space => space.Amenities)
                            .Include(space => space.Photos)
                            .FirstOrDefaultAsync(s => s.Id == spaceId);
        }
        public async Task<QueryResult<Space>> GetSpaces(SpaceQuery query)
        {
            var spaces =  _context.Spaces
                                    .Where(sp => sp.User.Id == query.UserId)
                                    .Include(sp => sp.User)
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
        public async Task<QueryResult<Space>> GetMerchantSpaces(SpaceQuery query)
        {
            var spaces =  _context.Spaces
                                    .Where(sp => sp.User.Id == query.UserId)
                                    // .Include(sp => sp.User)
                                    .Include(sp => sp.Photos)
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
        public async Task<QueryResult<Booking>> GetBookings(int userId, BookingQuery query)
        {
            var bookings =  _context.Bookings
                            .Where(b => b.UserId == userId)
                            .Where(b => b.Status == query.Status)
                            .Include(b => b.SpaceBooked)
                            .Include(b => b.Chat)
                            .AsQueryable();
            bookings = bookings.OrderByDescending(bp => bp.Id);
            bookings = FilterBookings(query, bookings);
            var queryResult = new QueryResult<Booking>();
            bookings = bookings.AsQueryable().Skip((query.CurrentPage - 1) * query.PageSize)
                                    .Take(query.PageSize);
            queryResult.TotalItems = bookings.Count();
            queryResult.Items = await bookings.ToListAsync();
            return queryResult;
        }
        public async Task<QueryResult<Booking>> GetCustomerBookings(int userId, BookingQuery query)
        {
            var bookings =  _context.Bookings
                            .Where(b => b.BookedById
                             == userId)
                            .Include(b => b.SpaceBooked)
                            .Include(b => b.Chat)
                            .AsQueryable();
            bookings = bookings.OrderByDescending(bp => bp.Id);
            var queryResult = new QueryResult<Booking>();
            // bookings = bookings.AsQueryable().Skip((query.CurrentPage - 1) * query.PageSize)
            //                         .Take(query.PageSize);
            queryResult.TotalItems = bookings.Count();
            queryResult.Items = await bookings.ToListAsync();
            return queryResult;
        }

        public async Task<List<Booking>> GetBookingDetails(int bookedById, BookingQuery query)
        {
            TimeSpan timeSpan20 = new TimeSpan(0, 20, 0);
            TimeSpan timeSpan10 = new TimeSpan(0, 10, 0);
            var bookingDetails = await _context.Bookings
                                .Where(b => b.BookedById == bookedById)
                                .Where(b => b.BookingTime.TimeOfDay <= query.TimeBooked.TimeOfDay - timeSpan20 || b.BookingTime.TimeOfDay >= query.TimeBooked.TimeOfDay - timeSpan10)
                                .ToListAsync();
            return bookingDetails;
        }
        private IQueryable<Space> FilterSpaces(SpaceQuery query, IQueryable<Space> spaces)
        {
            if(query.Price > 0)
                spaces = spaces.Where(sp => (sp.Price <= query.Price));
            if(query.Price > 500000)
                spaces = spaces.Where(sp => (sp.Price >= query.Price));
            if(!string.IsNullOrWhiteSpace(query.Location))
                spaces = spaces.Where(sp => sp.LocationAddress.ToLower().Contains(query.Location));
            if(!string.IsNullOrWhiteSpace(query.SearchString))
                spaces = spaces.Where(sp => sp.LocationAddress.ToLower().StartsWith(query.SearchString) || sp.LocationAddress.ToLower().Contains(query.SearchString) || sp.Name.StartsWith(query.SearchString) || sp.Name.ToLower().Contains(query.SearchString) || sp.Description.Contains(query.SearchString));
            if(query.Size > 0)
                spaces = spaces.Where(sp => Convert.ToInt64(sp.Size) >= query.Size);
            // if(!string.IsNullOrWhiteSpace(query.SpaceType) && query.SpaceType.Length > 0) 
            //     spaces = spaces.Where(sp => sp.Type.Type.ToLower().Contains(query.SpaceType.ToLower()));
            return spaces;            
        }
        private IQueryable<Booking> FilterBookings(BookingQuery query, IQueryable<Booking> booking)
        {
            if(!string.IsNullOrWhiteSpace(query.SearchString))
                booking = booking.Where(bk => bk.SpaceBooked.Name.ToLower().StartsWith(query.SearchString) || bk.Id.ToString() == query.SearchString);
            if(query.DateStart != null && query.DateStart.Date > DateTime.Parse("01/01/0001") && query.DateEnd != null && query.DateStart.Date > DateTime.Parse("01/01/0001"))
                booking = booking.Where(bk => bk.UsingFrom.Date >= query.DateStart.Date
                && bk.UsingFrom.Date <= query.DateEnd.Date);
            return booking;            
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
        // public async Task<IEnumerable<PricingOption>> GetPricingOptions()
        // {
        //     return await _context.PricingOptions.ToListAsync();
        // }
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
        public async Task<QueryResult<User>> GetMerchants()
        {
            var users = await _context.Users
                        .Where(u => u.Role == Role.Merchant)
                        .ToListAsync();            
            var queryResult = new QueryResult<User>();
            queryResult.TotalItems = users.Count();
            queryResult.Items = users;
            return queryResult;
        }

        public async void VerifyMerchant(int id)
        {
            var merchant = await _context.Users
                        .FirstOrDefaultAsync(u => u.Id == id);
            merchant.VerifiedAsMerchant = true;
        }
        public async void CreateBookingFromReservation(int id)
        {
            var reservation = await _context.Bookings
                                    .FirstOrDefaultAsync(bk => bk.Id == id);
            reservation.Status = BookingStatus.Booked;
        }
        public async Task<Booking> GetBooking(int bookingId)
        {
            return await _context.Bookings
                            .FirstOrDefaultAsync(b => b.Id == bookingId);
        }
        // public async Task<IEnumerable<Booking>> GetBookings(int userId)
        // {
        //     return await _context.Bookings
        //                     .Where(b => b.UserId == userId)
        //                     .ToListAsync();
        // }
        public async Task<List<BookedTimes>> GetBookedTimes(int spaceId, BookedTimes proposedBookingTime)
        {
            var bookings = await _context.Bookings
                            .Include(bk => bk.SpaceBooked)
                            .Where(bk => bk.SpaceBooked.Id == spaceId)
                            .Where(bk => bk.Status == BookingStatus.Booked)
                            .ToListAsync();
        //Check for the existing bookings that UsingFrom or UsingTill falls in between the proposed using from nd to
            var existing = new List<Booking>();
            foreach (var booking in bookings)
            {
                var fallsInBtw = ((proposedBookingTime.From >= booking.UsingFrom || proposedBookingTime.From <= booking.UsingFrom) && booking.UsingFrom <= proposedBookingTime.To);
                var alsoInBetween = ((proposedBookingTime.From >= booking.UsingTill || proposedBookingTime.From <= booking.UsingTill) && booking.UsingTill <= proposedBookingTime.To);
                if(fallsInBtw || alsoInBetween)
                    existing.Add(booking);
            };
            var bookingTimes = new List<BookedTimes>();
            foreach (var item in existing)
            {
                var bookingTime = new BookedTimes();
                bookingTime.From = item.UsingFrom;
                bookingTime.To = item.UsingTill;
                bookingTime.Status = item.Status.ToString();
                bookingTimes.Add(bookingTime);
            };
            return bookingTimes;
        }
        public async Task<Photo> GetPhoto(int id)
        {
            return await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<Photo> GetMainPhoto(int id)
        {
            return await _context.Photos.Where(p => p.IsMain)
                                        .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<MerchantMetrics> GetMerchantMetrics(int userId)
        {
            var metrics = new MerchantMetrics();
            var bookingsNdReservations = await _context.Bookings
                                            .Where(bk => bk.UserId == userId)
                                            .Include(bk => bk.SpaceBooked)
                                            .ToListAsync();
            var bookings = new List<Booking>();
            var reservations = new List<Booking>();
            var bookingOverviewDict = new Dictionary<int, UsageOverview>();

            bookingsNdReservations.ForEach(booking => {
                if(booking.Status == BookingStatus.Booked) {
                    bookings.Add(booking);
                    metrics.TotalRevenue += booking.TotalPrice;
                    // bookingOverviewDict.FirstOrDefault(x => x.Key == booking.SpaceBooked.Id);
                    var usageOverview = new UsageOverview {
                        SpaceId = booking.SpaceBooked.Id,
                        HoursUsed = (booking.UsingTill - booking.UsingFrom).TotalHours,
                        RevenueAccumulated = booking.TotalPrice
                    };
                    if(bookingOverviewDict.ContainsKey(booking.SpaceBooked.Id)) {
                        // var index = bookingsDict.Keys.ToList().IndexOf(booking.SpaceBooked.Id);
                        var previousValue = bookingOverviewDict[booking.SpaceBooked.Id];
                        var newValue = new UsageOverview{
                            SpaceId = previousValue.SpaceId,
                            RevenueAccumulated = previousValue.RevenueAccumulated + usageOverview.RevenueAccumulated,
                            HoursUsed = previousValue.HoursUsed + usageOverview.HoursUsed 
                        };
                        bookingOverviewDict[booking.SpaceBooked.Id] = usageOverview;
                    } 
                    else if(!bookingOverviewDict.ContainsKey(booking.SpaceBooked.Id)) {
                        bookingOverviewDict.Add(booking.SpaceBooked.Id, usageOverview);
                    }

                } 
                if(booking.Status == BookingStatus.Reserved){
                    reservations.Add(booking);
                }
            });
            double maxHours = 0, maxRev = 0;
            int spaceWithMaxHoursId = 0, spaceWithMaxRevId = 0;

            foreach (var item in bookingOverviewDict)
            {
                if (maxRev < item.Value.RevenueAccumulated)
                {
                    maxRev = item.Value.RevenueAccumulated;
                    spaceWithMaxRevId = item.Value.SpaceId;
                }
                if (maxHours < item.Value.HoursUsed)
                {
                    maxHours = item.Value.HoursUsed;
                    spaceWithMaxHoursId = item.Value.SpaceId;
                }
            }
            // var bookingOverviewListForMaxHours = bookingOverviewDict.Values.OrderBy(x => x.HoursUsed).ToList();
            // var bookingOverviewListForMaxRevenue = bookingOverviewDict.Values.OrderBy(x => x.RevenueAccumulated).ToList();
            metrics.TotalSpaceBooking = bookings.Count;
            metrics.TotalSpaceReservation = reservations.Count;

            metrics.TopSpaceHoursUsed = new TopSpaceUsage {
                SpaceId = spaceWithMaxHoursId,
                TotalUsageInHours = maxHours             
            };

            metrics.TopSpaceRevenueUsage = new TopSpaceUsage {
                SpaceId = spaceWithMaxRevId,
                TopSpaceRevenue = maxRev
            };
            return metrics;
        }
        public string ComposeMessage(MessageParams messageParams)
        {
            return "";
        }
    }
}