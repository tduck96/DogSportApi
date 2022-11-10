using System;
using RealPetApi.Data;
using RealPetApi.Dtos;
using RealPetApi.Interfaces;
using System.Security.Cryptography;
using RealPetApi.Models;
using AutoMapper;

namespace RealPetApi.Repositories
{
    public class DogRepository : IDogRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DogRepository(DataContext context,
            IMapper mapper
          )
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<bool> CreateDog(Dog dogToCreate)
        {
            await _context.Dogs.AddAsync(dogToCreate);
            var created = await _context.SaveChangesAsync();
            return created > 0;
        }

        public async Task<Dog> GetDog(int id)
        {
            return await _context.Dogs.FirstOrDefaultAsync(x => x.Id == id);

        }


        public async Task<ICollection<Dog>> GetDogs()
        {
            return await _context.Dogs.ToListAsync();
        }

        public async Task<bool> UpdateDog(Dog dogToUpdate)
        {
            _context.Dogs.Update(dogToUpdate);
            var updated = await _context.SaveChangesAsync();
            return updated > 0;

        }

        public async Task<bool> DeleteDog(int dogId)
        {
            var dog = await GetDog(dogId);
            _context.Dogs.Remove(dog);
            var deleted = await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<SportDto>> GetSportsByDog(int dogId)
        {
            var sports = await _context.DogSports.Where(c => c.DogId == dogId).Select(c => c.Sport).ToListAsync();

            var sportsMap = _mapper.Map<List<SportDto>>(sports);
            //List<SportDto> sportDtos = new List<SportDto>();

            //foreach (Sport sport in sports)
            //{

            //    var dto = new SportDto
            //    {
            //        Id = sport.Id,
            //        Name = sport.Name,
            //        PhotoUrl = sport.PhotoUrl,
            //    };

            //    sportDtos.Add(dto);
            //}
            //return sportDtos;
            return sportsMap;
        }

        public async Task<List<Title>> GetTitlesByDog(int dogId)
        {
            return await _context.DogTitles.Where(e => e.DogId == dogId).Select(c => c.Title).ToListAsync();
        }

        public async Task<List<DogPhotoDto>> GetPhotosByDog(int dogId)
        {
            var photos = await _context.DogPhotos.Where(c => c.DogId == dogId).ToListAsync();

            List<DogPhotoDto> photoDtos = new List<DogPhotoDto>();

            foreach(DogPhoto dogPhoto in photos)
            {
                var dto = new DogPhotoDto
                {
                    Id = dogPhoto.Id,
                    Url = dogPhoto.Url,
                    PublicId = dogPhoto.PublicId
                };

                photoDtos.Add(dto);
            }
            return photoDtos;
        }

        public async Task<bool> AddSportsToDog(DogSport dogsport)
        {
            await _context.DogSports.AddAsync(dogsport);
            var created = await _context.SaveChangesAsync();
            return created > 0;
        }
    }
}

