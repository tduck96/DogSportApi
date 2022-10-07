using System;
using RealPetApi.Models;

namespace RealPetApi.Repositories
{
    public class WallPostRepository : IWallPostRepository
    {
        private readonly DataContext _context;

        public WallPostRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateWallPost(WallPost wallPostCreate)
        {
            await _context.Wallposts.AddAsync(wallPostCreate);
            var created = await _context.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> DeleteWallPost(int wallpostId)
        {
            var wallpost = await GetWallPost(wallpostId);
            _context.Wallposts.Remove(wallpost);
            var deleted = await _context.SaveChangesAsync();
            return true;
        }

        public async Task<WallPost> GetWallPost(int wallpostId)
        {
            return await _context.Wallposts.FirstOrDefaultAsync(w => w.Id == wallpostId);
        }

        public async Task<List<Comment>> GetWallPostComments(int wallpostId)
        {
            return await _context.Comments.Where(c => c.WallPostId == wallpostId).ToListAsync();
        }

        public async Task<List<WallPost>> GetWallPosts()
        {
            return await _context.Wallposts.ToListAsync();
        }

        public async Task<bool> UpdateWallPost(WallPost wallPostUpdate)
        {
            _context.Wallposts.Update(wallPostUpdate);
            var updated = await _context.SaveChangesAsync();
            return updated > 0;
        }
    }
}

