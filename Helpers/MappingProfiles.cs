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

            CreateMap<Club, ClubCreateDto>();
            CreateMap<ClubCreateDto, Club>();

            CreateMap<Dog, DogDto>();
            CreateMap<DogDto, Dog>();

            CreateMap<Handler, HandlerDto>();
            CreateMap<HandlerDto, Handler>();

            CreateMap<Location, LocationDto>();
            CreateMap<LocationDto, Location>();

            CreateMap<Sport, SportDto>();
            CreateMap<SportDto, Sport>();

            CreateMap<Handler, AuthResponseDto>();
            CreateMap<AuthResponseDto, Handler>();

            CreateMap<HandlerUpdateDto, Handler>();
            CreateMap<Handler, HandlerUpdateDto>();

            CreateMap<Handler, HandlerCommentDto>();
            CreateMap<HandlerCommentDto, Handler>();

            CreateMap<Handler, HandlerProfileDto>();
            CreateMap<HandlerProfileDto, Handler>();

            CreateMap<Dog, DogDtoForUserProfile>();
            CreateMap<DogDtoForUserProfile, Dog>();

            CreateMap<Title, TitleDto>();
            CreateMap<TitleDto, Title>();

            CreateMap<Comment, CommentDto>();
            CreateMap<CommentDto, Comment>();

            CreateMap<WallPost, WallPostDto>();
            CreateMap<WallPostDto, WallPost>();

            CreateMap<WallPost, WallPostCreateDto>();
            CreateMap<WallPostCreateDto, WallPost>();

            CreateMap<Photo, PhotoDto>();
            CreateMap<PhotoDto, Photo>();

            CreateMap<UserProfile, UserCreateDto>();
            CreateMap<UserCreateDto, UserProfile>();

            CreateMap<UserProfile, UserListDto>();
            CreateMap<UserListDto, UserProfile>();

            CreateMap<UserProfile, HandlerProfileDto>();

            CreateMap<WallPost, WallPostProfileDto>();


        }
    }
}

