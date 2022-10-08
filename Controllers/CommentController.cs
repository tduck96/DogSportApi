using System;
using System.Web.Http.ModelBinding;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealPetApi.Dtos;
using RealPetApi.Models;

namespace RealPetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IWallPostRepository _wallPostRepository;
        private readonly IHandlerRepository _handlerRepository;
        private readonly IMapper _mapper;

        public CommentController(ICommentRepository commentRepository,
            IWallPostRepository wallPostRepository,
            IHandlerRepository handlerRepository,
            IMapper mapper)
        {
            _commentRepository = commentRepository;
            _wallPostRepository = wallPostRepository;
            _handlerRepository = handlerRepository;
            _mapper = mapper;
        }


        [HttpGet("{commentId}")]
        [ProducesResponseType(200, Type = typeof(CommentDto))]
        [ProducesResponseType(400)]

        public async Task<ActionResult<CommentDto>> GetComment(int commentId)
        {
            var comment = await _commentRepository.GetComment(commentId);
            var commentToReturn = _mapper.Map<CommentDto>(comment);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(commentToReturn);

        }



        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public async Task<ActionResult<bool>> CreateComment(
            [FromBody] CommentDto commentCreate,
            [FromQuery] int handlerId,
            [FromQuery] int wallPostID
            )
        {


            if (commentCreate == null)
                return BadRequest(ModelState);

           var wallpost = await _wallPostRepository.GetWallPost(wallPostID);
           var handler = await _handlerRepository.GetHandler(handlerId);

            var comment = new Comment
            {
                Id = commentCreate.Id,
                Body = commentCreate.Body,
                HandlerId = handlerId,
                WallPostId = wallPostID,
                WallPost = wallpost,
                Handler = handler

            };
           
           

            await _commentRepository.CreateComment(comment);
           

            return Ok("Sucessfully added");


        }

        [HttpPut("{commentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public async Task<ActionResult<bool>> UpdateComment(int commentId,
            [FromQuery] int handlerId,
            [FromQuery] int wallpostId,
            [FromBody] CommentDto request)
        {

            var handler = await _handlerRepository.GetHandler(handlerId);
            var wallpost = await _wallPostRepository.GetWallPost(wallpostId);

            var updatedComment = new Comment
            {
                Id = request.Id,
                Body = request.Body,
                HandlerId = handlerId,
                Handler = handler,
                WallPostId = wallpostId,
                WallPost = wallpost
            };

            var updated = await _commentRepository.UpdateComment(updatedComment);

            if (updated)
                return Ok();

            return NotFound();
        }

        [HttpDelete("{commentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public async Task<ActionResult<bool>> DeleteComment(int commentId)
        {
            var commentToDelete = await _commentRepository.GetComment(commentId);

            if (commentToDelete != null)
            {
                await _commentRepository.DeleteComment(commentId);
                return Ok("Comment Removed");

            }

            return NotFound();

        }

    }
}

