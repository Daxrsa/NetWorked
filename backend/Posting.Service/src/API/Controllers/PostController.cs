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
            return Ok(await _postService.GetPosts());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostDTO>> GetPostById(Guid id)
        {
            return Ok(await _postService.GetPostById(id));
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
                    return Ok(await _postService.AddPost(postDto));
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
            return Ok(await _postService.UpdatePost(id, request));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<PostDTO>>> DeletePost(Guid id)
        {
            return Ok(await _postService.DeletePost(id));
        }
    }
}
