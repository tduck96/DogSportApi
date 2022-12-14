using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealPetApi.Dtos;
using RealPetApi.Models;

namespace RealPetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class WallPostController : Controller
    {
        private readonly IWallPostRepository _wallPostRepository;
        private readonly IMapper _mapper;
        private readonly IHandlerRepository _handlerRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IUserProfileRespository _userProfileRespository;

        public WallPostController(
            IWallPostRepository wallPostRepository,
            IMapper mapper,
            IHandlerRepository handlerRepository,
            ICommentRepository commentRepository,
            IUserProfileRespository userProfileRespository)
  
        {
            _wallPostRepository = wallPostRepository;
            _mapper = mapper;
            _handlerRepository = handlerRepository;
            _commentRepository = commentRepository;
            _userProfileRespository = userProfileRespository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<WallPostDto>))]

        public async Task<ActionResult<WallPostDto>> GetWallPosts()
        {
            var wallPosts = await _wallPostRepository.GetWallPosts();

            return Ok(wallPosts);
        }

        [HttpGet("{wallpostId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<WallPostDto>))]

        public async Task<ActionResult<WallPostDto>> GetWallPost(int wallpostId)
        {
            var wallPost = await _wallPostRepository.GetWallPost(wallpostId);
            var comments = await _wallPostRepository.GetWallPostComments(wallpostId);
            var commentsToDto = _mapper.Map<List<CommentDto>>(comments);
            var user = await _userProfileRespository.GetUser(wallPost.UserProfileId);
       

            var wallpostDto = new WallPostDto
            {
                Id = wallPost.Id,
                Body = wallPost.Body,
                PhotoUrl = wallPost.PhotoUrl,
                Comments = commentsToDto,
                Name = user.Name,
                UserPhoto = user.PhotoUrl

            };


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(wallpostDto);
        }


        [HttpPut("{wallPostId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> UpdateWallPost(int wallPostId,
            [FromQuery] int userId,
            [FromBody] WallPostUpdateDto updatedPost)
            
        {
            var comments = await _wallPostRepository.GetWallPostComments(wallPostId);
            

            var post = new WallPost
            {
                Id = updatedPost.Id,
                Body = updatedPost.Body,
                PhotoUrl = updatedPost.PhotoUrl,
                UserProfileId = userId,
                Comments = comments,
                Date = DateTime.Now
            };



            var updated = await _wallPostRepository.UpdateWallPost(post);

            if (updated)
                return Ok("Record Updated");
            return NotFound();


        }


        [HttpPost("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public async Task<ActionResult> CreateWallPost(
            int userId,
           [FromBody] WallPostCreateDto newPost)

        {

            if (newPost == null)
                return BadRequest(ModelState);

            if (newPost == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var postMap = _mapper.Map<WallPost>(newPost);

          
            postMap.UserProfile = await _userProfileRespository.GetUser(userId);
            postMap.Date = DateTime.Now;

            await _wallPostRepository.CreateWallPost(postMap);

            return Ok("Sucessfully added new wallpost to records");


        }

        [HttpGet("following/{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<WallPostProfileDto>))]

        public async Task<ActionResult<WallPostProfileDto>> GetFollowingWallposts(int userId)
        {
            var wallPosts = await _wallPostRepository.GetFollowerWallposts(userId);

            if (wallPosts == null)
                return NotFound();

            return Ok(wallPosts);
        }



        [HttpDelete("{wallpostId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public async Task<ActionResult<bool>> DeleteWallPost(int wallpostId)
        {
            var deleted = await _wallPostRepository.DeleteWallPost(wallpostId);
            if (deleted)
                return Ok("Handler deleted");


            return NotFound();

        }


    }
}

