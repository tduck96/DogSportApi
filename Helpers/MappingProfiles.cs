using System;
using AutoMapper;
using RealPetApi.Dtos;
using RealPetApi.Models;

namespace RealPetApi.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Breed, BreedDto>();
            CreateMap<BreedDto, Breed>();

            CreateMap<Club, ClubDto>();
            CreateMap<ClubDto, Club>();

            CreateMap<Dog, DogDto>();
            CreateMap<DogDto, Dog>();

            CreateMap<Handler, HandlerDto>();
            CreateMap<HandlerDto, Handler>();

            CreateMap<Location, LocationDto>();
            CreateMap<LocationDto, Location>();

            CreateMap<Sport, SportDto>();
            CreateMap<SportDto, Sport>();
        }
    }
}

