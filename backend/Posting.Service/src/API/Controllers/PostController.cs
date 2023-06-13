using Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : BaseApiController
    {
        private readonly HttpClient _httpClient;
        private readonly string _userMicroserviceUrl;
        private readonly IPostService _postService;
        public PostController(IPostService postService, HttpClient httpClient, IConfiguration configuration)
        {
            _postService = postService;
            _httpClient = httpClient;
             _userMicroserviceUrl = configuration.GetValue<string>("UserMicroserviceUrl");
        }

        [HttpGet("getLoggedInUserFromUserService")]
        public async Task<ActionResult<UserDTO>> GetLoggedInUser()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_userMicroserviceUrl}/getLoggedInUser");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var loggedInUser = JsonConvert.DeserializeObject<UserDTO>(content);
                    return Ok(loggedInUser);
                }

                return BadRequest("Failed to retrieve the logged-in user.");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, ex.Message);
            }
        }

        // Other endpoints and actions for the PostController

        [HttpGet]
        public async Task<ActionResult<List<PostDTO>>> GetAllPosts()
        {
            return HandleResult(await _postService.GetPosts());
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<PostDTO>> GetPostById(Guid id)
        {
            return HandleResult(await _postService.GetPostById(id));
        }

        [HttpPost("add")]
        public async Task<ActionResult<List<PostDTO>>> AddPost([FromForm]CreatePostDto postDto)
        {
            return HandleResult(await _postService.AddPost(postDto));
        }

        [HttpPut("{id}/edit")]
        public async Task<ActionResult<List<PostDTO>>> UpdatePost(Guid id, PostDTO request)
        {
            return HandleResult(await _postService.UpdatePost(id, request));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<PostDTO>>> DeletePost(Guid id)
        {
            return HandleResult(await _postService.DeletePost(id));
        }
    }
}
