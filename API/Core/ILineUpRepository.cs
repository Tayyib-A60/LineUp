using System.Collections.Generic;
using System.Threading.Tasks;
using API.Core.Models;

namespace API.Core
{
    public interface ILineUpRepository
    {
         void Add<T>(T entity) where T : class;
         void Update<T>(T entity) where T : class;
         void Delete<T>(T entity) where T : class;
         Task<bool> EntityExists<T>(T entityName) where T: class;
         Task<bool> SaveAllChanges();
         Task<Enquiry> GetEnquiry(int enquiryId);
         Task<Space> GetSpace(int spaceId);
         Task<QueryResult<Space>> GetSpaces(SpaceQuery query);
         Task<SpaceType> GetSpaceType(int spaceTypeId);
         Task<IEnumerable<SpaceType>> GetSpaceTypes();
         Task<IEnumerable<Enquiry>> GetEnquiries(int userId);
         Task<Amenity> GetAmenity(int amenityId);
         Task<IEnumerable<Amenity>> GetAmenities();
         Task<Booking> GetBooking(int bookingId);
         Task<IEnumerable<Booking>> GetBookings(int userId);
    }
}