using System.Net.Http.Headers;
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Authorize(Roles = "Applicant")]
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : BaseApiController
    {
        private readonly HttpClient _httpClient;
        private readonly IPostService _postService;
        public PostController(IPostService postService, HttpClient httpClient, IConfiguration configuration)
        {
            _postService = postService;
            _httpClient = httpClient;
        }

        [HttpGet("getLoggedInUserFromUserService")]
        public async Task<ActionResult<UserDTO>> GetLoggedInUser()
        {
            try
            {
                // var authorizationHeader = Request.Headers["Authorization"].ToString();

                // if (string.IsNullOrEmpty(authorizationHeader))
                // {
                //     return BadRequest("Authorization header is missing.");
                // }

                // var tokenParts = authorizationHeader.Split(' ');

                // if (tokenParts.Length < 2)
                // {
                //     return BadRequest("Invalid Authorization header format.");
                // }

                // var token = tokenParts[1];
               var token = Request.Headers["Authorization"].ToString().Split(' ')[1];

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.GetAsync("http://localhost:5116/api/Auth/GetloggedInUser");

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
                return StatusCode(500, ex.Message);
            }
        }

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
        public async Task<ActionResult<List<PostDTO>>> AddPost([FromForm] CreatePostDto postDto)
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
