using Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : BaseApiController
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
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
        public async Task<ActionResult<List<PostDTO>>> AddPost(PostDTO postDto)
        {
            return HandleResult(await _postService.AddPost(postDto));
        }

        [HttpPut("{id}/edit")]
        public async Task<ActionResult<List<PostDTO>>> UpdatePost(Guid id, PostDTO request)
        {
            return HandleResult(await _postService.UpdatePost(id, request));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<PostDTO>>> DeleteExam(Guid id)
        {
            return HandleResult(await _postService.DeletePost(id));
        }
    }
}