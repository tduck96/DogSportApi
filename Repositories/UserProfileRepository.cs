using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RealPetApi.Dtos;
using RealPetApi.Models;

namespace RealPetApi.Repositories
{
    public class UserProfileRepository : IUserProfileRespository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserProfileRepository(DataContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> CreateUser(UserProfile user)
        {
            await _context.UserProfiles.AddAsync(user);

            var created = await _context.SaveChangesAsync();

            UserFollowing userfollowing = new UserFollowing
            {
                UserProfileId = user.Id,
                UserFollowsId = 24
            };

            if (created > 0)
            {
                await FollowUser(userfollowing);
                return true;
            }
           
            else return false;
        }

        public async Task<List<DogDtoForUserProfile>> GetDogsByUser(int userId)
        {
            var dogs = await _context.Dogs.Where(c => c.UserProfileId == userId).ToListAsync();

            var dogMap = _mapper.Map<List<DogDtoForUserProfile>>(dogs);

            return dogMap;
        }

        public async Task<List<PhotoDto>> GetPhotosByUser(int userId)
        {
            var photos = await _context.Photos.Where(c => c.UserProfileId == userId).ToListAsync();

            var photoMap = _mapper.Map<List<PhotoDto>>(photos);

            return photoMap;
        }

        public async Task<UserProfile> GetUser(int userId)
        {
           return await _context.UserProfiles.Where(c => c.Id == userId).Include(c => c.Location).FirstOrDefaultAsync();

        }

        public async Task<Handler> GetUserHandler(int userId)
        {
            var user = await GetUser(userId);
            var handlerId = user.HandlerId;

            return await _context.Handlers.Where(c => c.Id == handlerId).FirstOrDefaultAsync();
        }


        public async Task<IEnumerable<UserListDto>> GetUsers()
        {
            var users = await _context.UserProfiles
                .Join(_context.Locations,
                p => p.LocationId,
                l => l.Id,
                (p, l) =>

                new UserListDto
                {

                    Id = p.Id,
                    Name = p.Name,
                    Bio = p.Bio,
                    photoUrl = p.PhotoUrl,
                    Location = l.Name

                })

                .ToListAsync();

            return users;



        }




        public async Task<bool> UpdateUserInfo(UserProfile user)
        {
            _context.UserProfiles.Update(user);
            var updated = await _context.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<List<WallPostProfileDto>> GetWallPostsByUser(int userId)
        {
            var wallposts = await _context.Wallposts.Where(c => c.UserProfileId == userId).ToListAsync();
            var wallPostMap = _mapper.Map<List<WallPostProfileDto>>(wallposts);
          
            return wallPostMap;
        }

        public async Task<bool> AddUserSport(HandlerSport sport)
        {
            await _context.HandlerSports.AddAsync(sport);
            var created = await _context.SaveChangesAsync();
            return created > 0;
        }

        public async Task<List<Sport>> GetSportsByUser(int id)
        {
            return await _context.HandlerSports.Where(c => c.UserProfileId == id).Select(c => c.Sport).ToListAsync();
        }

        public async Task<bool> RemoveUserSport(HandlerSport handler)
        { 

             _context.HandlerSports.Remove(handler);
            var deleted = await _context.SaveChangesAsync();
            return true;
        }

        public async Task<HandlerSport> GetHandlerSport(int userId, int sportId)
        {
            return await _context.HandlerSports
                .Where(c => c.UserProfileId == userId && c.SportId == sportId)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> FollowUser(UserFollowing user)
        {
            await _context.UserFollowing.AddAsync(user);
            var created = await _context.SaveChangesAsync();

            return created > 0;
        }


        public async Task<bool> UnfollowUser(int userId, int followId)
        {
            var follower = await GetFollower(userId, followId);

            _context.UserFollowing.Remove(follower);

            var deleted = await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<UserListDto>> GetUserFollowing(int userId)
        {
            var following = await _context.UserFollowing
                .Where(c => c.UserProfileId == userId)
                .Join(_context.UserProfiles,
                uf => uf.UserFollowsId,
                up => up.Id,
                (uf, up) => new { uf, up })
                .Join(_context.Locations,
                upp => upp.up.LocationId,
                ufp => ufp.Id,
                (upp, ufp) => new { upp, ufp })

                .Select(result => new UserListDto
                {
                    Id = result.upp.up.Id,
                    Name = result.upp.up.Name,
                    Bio = result.upp.up.Bio,
                    photoUrl = result.upp.up.PhotoUrl,
                    Location = result.ufp.Name

                })
                .ToListAsync();


            return following;

        }

        public async Task<bool> FollowChecker(int userId, int followingId)
        {
            var result = await _context.UserFollowing.Where(c => c.UserProfileId == userId && c.UserFollowsId == followingId).FirstOrDefaultAsync();

            if (result == null)
            {
                return false;
            }

            return true;
        }

        public async Task<UserFollowing> GetFollower(int userId, int followId)
        {
            return await _context.UserFollowing.Where(c => c.UserProfileId == userId && c.UserFollowsId == followId).FirstOrDefaultAsync();
        }
    }
}

