using System.Net.Http.Headers;
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
    public class CommentController : BaseApiController
    {
        private readonly ICommentService _commentService;
        private readonly HttpClient _httpClient;

        public CommentController(ICommentService commentService, HttpClient httpClient)
        {
            _commentService = commentService;
            _httpClient = httpClient;
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
        public async Task<ActionResult<List<CommentDTO>>> AddCommentToPost(Guid postId, CommentDTO commentDto)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString().Split(' ')[1];

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.GetAsync("http://localhost:5116/api/Auth/GetloggedInUser");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var loggedInUser = JsonConvert.DeserializeObject<UserDTO>(content);
                    commentDto.Author = loggedInUser.Username;
                    return HandleResult(await _commentService.AddCommentToPost(postId, commentDto));
                }

                return BadRequest("Failed to retrieve the logged-in user.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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