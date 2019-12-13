using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Core;
using API.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Persistence
{
    public class PhotoRepository : IPhotoRepository
    {
        private LineUpContext  _context { get; }
        public PhotoRepository(LineUpContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Photo>> GetSpacePhotos(int spaceId)
        {
            return await _context.Photos
                    .Where(p => p.SpaceId == spaceId)
                    .ToListAsync(); 
        }
        public async Task<Photo> GetUserPhoto(int userId)
        {
            throw new NotImplementedException();
            // return await _context.Photos
            //                 .SingleOrDefaultAsync(p => p.UserId == userId);
        }
        public async Task<Photo> GetSpacePhoto(int spaceId)
        {
            return await _context.Photos.SingleOrDefaultAsync(p => p.SpaceId == spaceId && p.IsMain == true);
        }
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
        
    }
}