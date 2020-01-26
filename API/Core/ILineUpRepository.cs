using System.Collections.Generic;
using System.Threading.Tasks;
using API.Core.Models;

namespace API.Core
{
    public interface ILineUpRepository
    {
         void Add<T>(T entity) where T : class;
         void Update<T>(T entity) where T : class;
         void UpdateSpace(Space space);
         void Delete<T>(T entity) where T : class;
         Task<bool> EntityExists<T>(T entityName) where T: class;
         Task<bool> SaveAllChanges();
         Task<Enquiry> GetEnquiry(int enquiryId);
         Task<Space> GetSpace(int spaceId);
         Task<Photo> GetPhoto(int id);
         Task<Photo> GetMainPhoto(int id);
         void CreateBookingFromReservation(int id);
         Task<QueryResult<Space>> GetSpaces(SpaceQuery query);
         Task<QueryResult<Space>> GetMerchantSpaces(SpaceQuery query);
         Task<QueryResult<Booking>> GetBookings(int id, BookingQuery query);
         Task<QueryResult<User>> GetMerchants();
         void VerifyMerchant(int id);
         Task<QueryResult<Booking>> GetCustomerBookings(int id, BookingQuery query);
         Task<SpaceType> GetSpaceType(int spaceTypeId);
         Task<IEnumerable<SpaceType>> GetSpaceTypes();
         Task<IEnumerable<Enquiry>> GetEnquiries(int userId);
         Task<Amenity> GetAmenity(int amenityId);
         Task<IEnumerable<Amenity>> GetAmenities();
         Task<List<BookedTimes>> GetBookedTimes(int spaceId, BookedTimes proposedBookingTime);
         Task<Booking> GetBooking(int bookingId);
         Task<List<Booking>> GetBookingDetails(int bookedById, BookingQuery query);
         string ComposeMessage(MessageParams messageParams);
         Task<MerchantMetrics> GetMerchantMetrics(int userId);
         Task<IEnumerable<PricingOption>> GetPricingOptions();
        //  Task<IEnumerable<Booking>> GetBookings(int userId);
    }
}