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

        public WallPostController(
            IWallPostRepository wallPostRepository,
            IMapper mapper,
            IHandlerRepository handlerRepository,
            ICommentRepository commentRepository)
  
        {
            _wallPostRepository = wallPostRepository;
            _mapper = mapper;
            _handlerRepository = handlerRepository;
            _commentRepository = commentRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<WallPost>))]

        public async Task<ActionResult<WallPostDto>> GetWallPosts()
        {
            var wallPosts = await _wallPostRepository.GetWallPosts();


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(wallPosts);
        }

        [HttpGet("{wallpostId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<WallPost>))]

        public async Task<ActionResult<WallPostDto>> GetWallPost(int wallpostId)
        {
            var wallPost = await _wallPostRepository.GetWallPost(wallpostId);

            var handler = await _handlerRepository.GetHandler(wallPost.HandlerId);
            var handlerDto = _mapper.Map<HandlerCommentDto>(handler);

            var comments = await _wallPostRepository.GetWallPostComments(wallpostId);
            var commentsToDto = _mapper.Map<List<CommentDto>>(comments);

            var wallpostDto = new WallPostDto
            {
                Id = wallPost.Id,
                Body = wallPost.Body,
                PhotoUrl = wallPost.PhotoUrl,
                Handler = handlerDto,
                Comments = commentsToDto
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
            [FromQuery] int handlerId,
            [FromBody] WallPostUpdateDto updatedPost)
            
        {
            var comments = await _wallPostRepository.GetWallPostComments(wallPostId);
            

            var post = new WallPost
            {
                Id = updatedPost.Id,
                Body = updatedPost.Body,
                PhotoUrl = updatedPost.PhotoUrl,
                HandlerId = handlerId,
                Comments = comments
            };



            var updated = await _wallPostRepository.UpdateWallPost(post);

            if (updated)
                return Ok("Record Updated");
            return NotFound();


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

