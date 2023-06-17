using System.Net.Http.Headers;
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers
{
    //[Authorize(Roles = "Applicant")]
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : BaseApiController
    {
        private readonly IPostService _postService;
        private readonly HttpClient httpClient;

        public PostController(IPostService postService, HttpClient httpClient)
        {
            _postService = postService;
            this.httpClient = httpClient;
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
            try
            {
                var token = Request.Headers["Authorization"].ToString().Split(' ')[1];

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await httpClient.GetAsync("http://localhost:5116/api/Auth/GetloggedInUser");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var loggedInUser = JsonConvert.DeserializeObject<UserDTO>(content);
                    postDto.Username = loggedInUser.Username;
                    return HandleResult(await _postService.AddPost(postDto)); 
                }

                return BadRequest("Failed to retrieve the logged-in user.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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

        [HttpGet("filter-posts")]
        public async Task<ActionResult<List<PostDTO>>> FilterPosts() //for user's profile posts
        {
             try
            {
                var token = Request.Headers["Authorization"].ToString().Split(' ')[1];

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await httpClient.GetAsync("http://localhost:5116/api/Auth/GetloggedInUser");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var loggedInUser = JsonConvert.DeserializeObject<UserDTO>(content);
                    var username = loggedInUser.Username;
                    return HandleResult(await _postService.FilterPostsByUser(username)); 
                }

                return BadRequest("Failed to retrieve the logged-in user.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("countPosts")]
        public async Task<ActionResult<int>> CountPosts()
        {
            return Ok(await _postService.GetPostCount());
        }
    }
}
