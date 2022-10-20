using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealPetApi.Dtos;
using RealPetApi.Models;

namespace RealPetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : Controller
    {
        private readonly IUserProfileRespository _userProfileRespository;
        private readonly IHandlerRepository _handlerRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public UserController(IUserProfileRespository userProfileRespository,
            IHandlerRepository handlerRepository,
            ILocationRepository locationRepository,
            IMapper mapper
            )
        {
            _userProfileRespository = userProfileRespository;
            _handlerRepository = handlerRepository;
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        [HttpPost("{handlerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public async Task<ActionResult<bool>> CreateUserProfile(int handlerId,
             [FromBody] UserCreateDto user)
        {
            if (user == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userMap = _mapper.Map<UserProfile>(user);
            userMap.Handler = await _handlerRepository.GetHandler(handlerId);
            userMap.Location = await _locationRepository.GetLocation(user.LocationId);

            await _userProfileRespository.CreateUser(userMap);

            return Ok("Sucessfully added new profile to records");
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(UserProfile))]
        [ProducesResponseType(400)]

        public async Task<ActionResult<UserListDto>> GetUsers()
        {
            var users = await _userProfileRespository.GetUsers();
            if (users == null)
                return NotFound();



            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);

        }


        [HttpGet("profile/{handlerId}")]

        [ProducesResponseType(200, Type = typeof(UserProfile))]
        [ProducesResponseType(400)]

        public async Task<ActionResult<HandlerProfileDto>> GetUserProfile(int handlerId)
        {
            var user = await _handlerRepository.GetUserProfile(handlerId);

            if (user == null)
                return NotFound();


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userMap = _mapper.Map<HandlerProfileDto>(user);
            userMap.Photos = await _userProfileRespository.GetPhotosByUser(user.Id);
            userMap.WallPosts = await _userProfileRespository.GetWallPostsByUser(user.Id);
            userMap.Dogs = await _userProfileRespository.GetDogsByUser(user.Id);


            return Ok(userMap);

        }


        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(UserProfile))]
        [ProducesResponseType(400)]

        public async Task<ActionResult<HandlerProfileDto>> GetUser(int userId)
        {
            var user = await _userProfileRespository.GetUser(userId);
            if (user == null)
                return NotFound();

             var userMap = _mapper.Map<HandlerProfileDto>(user);
            userMap.Photos = await _userProfileRespository.GetPhotosByUser(user.Id);
            userMap.WallPosts = await _userProfileRespository.GetWallPostsByUser(user.Id);
            userMap.Dogs = await _userProfileRespository.GetDogsByUser(user.Id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(userMap);

        }

        [HttpPut("{handlerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<bool>> UpdateUser(int handlerId,
            [FromBody] UserCreateDto user)
        {
            if (user == null) return BadRequest(ModelState);

            var userMap = _mapper.Map<UserProfile>(user);
            userMap.Handler = await _handlerRepository.GetHandler(handlerId);
        

            var updated = await _userProfileRespository.UpdateUserInfo(userMap);


            if (updated)
                return Ok("Record Updated");

            return NotFound();


        }


    }
}

