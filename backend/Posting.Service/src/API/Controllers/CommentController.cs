using Application.DTOs;
using Application.Services.CommentService;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Persistence;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly DataContext _context;
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public CommentController(ICommentService commentService, DataContext context, HttpClient httpClient, IMapper mapper)
        {
            _commentService = commentService;
            _context = context;
            _httpClient = httpClient;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CommentDTO>>> GetComments(Guid postId)
        {
            return Ok(await _commentService.GetComments(postId));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CommentDTO>> GetCommentById(int id)
        {
            return Ok(await _commentService.GetCommentById(id));
        }

        [HttpPost("add")]
        // public async Task<ActionResult<List<CommentDTO>>> AddComment(Guid postId, CommentDTO commentDto)
        // {
        //     return HandleResult(await _commentService.AddComment(postId, commentDto));
        // }
        public async Task<ActionResult<CommentDTO>> AddComment(Guid postId, CommentDTO commentDto)
        {
            try
            {
                var post = await _context.Posts.FindAsync(postId);

                if (post == null)
                {
                    return BadRequest("Post not found.");
                }

                // Retrieve user information from the User microservice using an API call
                var userMicroserviceUrl = "http://localhost:5116/api/Auth/GetloggedInUser";
                var response = await _httpClient.GetAsync(userMicroserviceUrl);

                if (!response.IsSuccessStatusCode)
                {
                    return BadRequest("Failed to retrieve the logged-in user information.");
                }

                var userResponseContent = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<UserDTO>(userResponseContent);

                // Create the comment DTO object with the retrieved user information
                var commentDtoWithUser = new CommentDTO
                {
                    Author = user.Username,
                    Body = commentDto.Body
                };

                var comment = _mapper.Map<Comment>(commentDtoWithUser);
                comment.Post = post;

                _context.Comments.Add(comment);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result)
                {
                    return BadRequest("Failed to add the comment.");
                }

                var comments = await _context.Comments
                    .Where(x => x.Post.Id == postId)
                    .ProjectTo<CommentDTO>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return Ok(comments);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("{id}/edit")]
        public async Task<ActionResult<List<CommentDTO>>> UpdateComment(int id, CommentDTO request)
        {
            return Ok(await _commentService.UpdateComment(id, request));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<CommentDTO>>> DeleteComment(int id)
        {
            return Ok(await _commentService.DeleteComment(id));
        }
    }
}