using System.Collections.Generic;
using System.Threading.Tasks;
using API.Core.Models;

namespace API.Core
{
    public interface IPhotoRepository
    {
        Task<IEnumerable<Photo>> GetSpacePhotos(int spaceId);
         Task<Photo> GetUserPhoto(int userId);
         Task<Photo> GetSpacePhoto(int spaceId);
         Task CompleteAsync();
    }
}