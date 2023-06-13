using Application.DTOs;
using Application.Services.CommentService;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : BaseApiController
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CommentDTO>>> GetComments(PostDTO request)
        {
            return HandleResult(await _commentService.GetComments(request));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CommentDTO>> GetCommentById(int id)
        {
            return HandleResult(await _commentService.GetCommentById(id));
        }

        [HttpPost("add")]
        public async Task<ActionResult<List<CommentDTO>>> AddComment(CommentDTO commentDto)
        {
            return HandleResult(await _commentService.AddComment(commentDto));
        }

        [HttpPut("{id}/edit")]
        public async Task<ActionResult<List<CommentDTO>>> UpdateComment(int id, CommentDTO request)
        {
            return HandleResult(await _commentService.UpdateComment(id, request));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<CommentDTO>>> DeleteComment(int id)
        {
            return HandleResult(await _commentService.DeleteComment(id));
        }
    }
}