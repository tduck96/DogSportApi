using System;
using AutoMapper;
using RealPetApi.Dtos;
using RealPetApi.Models;

namespace RealPetApi.Repositories
{
    public class WallPostRepository : IWallPostRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public WallPostRepository(DataContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public async Task<IEnumerable<WallPostProfileDto>> GetFollowerWallposts(int userId)
        {
            var wallposts = await _context.UserFollowing
               .Where(c => c.UserProfileId == userId)

               .Join(_context.Wallposts,
                following => following.UserFollowsId,
                wallpost => wallpost.UserProfileId,
               (following, wallpost) => new { following, wallpost })

                .Join(_context.UserProfiles,
               userfollowingpost => userfollowingpost.following.UserFollowsId,
               userprofile => userprofile.Id,
               (userfollowingpost, userprofile) => new { userfollowingpost, userprofile })



               .Select(result => new WallPostProfileDto
               {
                   Id = result.userfollowingpost.wallpost.Id,
                   Body = result.userfollowingpost.wallpost.Body,
                   Name = result.userprofile.Name,
                   PhotoUrl = result.userfollowingpost.wallpost.PhotoUrl,
                   UserPhotoUrl = result.userprofile.PhotoUrl,
                   Date = result.userfollowingpost.wallpost.Date,
                   UserId = result.userprofile.Id

               })

                .OrderByDescending(wallpost => wallpost.Date)



               .ToListAsync();

            return wallposts;
        }

        public async Task<WallPost> GetWallPost(int wallpostId)
        {
           return await _context.Wallposts.FirstOrDefaultAsync(w => w.Id == wallpostId);


        }

        public async Task<ICollection<Comment>> GetWallPostComments(int wallpostId)
        {
            return await _context.Comments.Where(c => c.WallPostId == wallpostId).ToListAsync();
        }

        public async Task<List<WallPostDto>> GetWallPosts()
        {
           var wallposts = await _context.Wallposts.ToListAsync();

            List<WallPostDto> Dtos = new List<WallPostDto>();

            foreach (WallPost wallpost in wallposts)
            {
                var comments = await _context.Comments.Where(c => c.WallPostId == wallpost.Id).ToListAsync();
                var commentsToReturn = _mapper.Map<List<CommentDto>>(comments);

                var user = await _context.UserProfiles.Where(h => h.Id == wallpost.Id).FirstOrDefaultAsync();
                var userToReturn = _mapper.Map<HandlerCommentDto>(user);

                var dto = new WallPostDto
                {
                    Id = wallpost.Id,
                    Body = wallpost.Body,
                    PhotoUrl = wallpost.PhotoUrl,
                    Comments = commentsToReturn

                };
                Dtos.Add(dto);
                
            }

            return Dtos;
        }

        public async Task<bool> UpdateWallPost(WallPost wallPostUpdate)
        {
            _context.Wallposts.Update(wallPostUpdate);
            var updated = await _context.SaveChangesAsync();
            return updated > 0;
        }
    }
}

