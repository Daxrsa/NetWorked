using Common.Library;
using Microsoft.AspNetCore.Mvc;
using Posting.Service.Models;
using static Posting.Service.DTOs;

namespace Posting.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IRepo<Post> _postRepo;
        public PostController(IRepo<Post> postRepo)
        {
            _postRepo = postRepo;
        }

        [HttpGet]
        public async Task<IEnumerable<PostDTO>> GetAsync()
        {
            var posts = (await _postRepo.GetAllAsync()).Select(post => post.AsDto());
            return posts;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostDTO>> GetSingleAsync(Guid id)
        {
            var post = await _postRepo.GetAsync(id);
            if (post == null)
            {
                return NotFound("This job does not exist.");
            }
            return post.AsDto();
        }

        [HttpPost("add")]
        public async Task<ActionResult<PostDTO>> PostAsync(PostDTO postDTO)
        {
            var post = new Post
            {
            Description = postDTO.Description,
            FilePath = postDTO.FilePath
            };
            await _postRepo.CreateAsync(post);
            return CreatedAtAction(nameof(GetSingleAsync), new { id = post.Id }, post);
        }

        [HttpPut("{id}/edit")]
        public async Task<IActionResult> PutAsync(Guid id, PostDTO postDTO)
        {
            var existingPost = await _postRepo.GetAsync(id);
            if (existingPost == null)
            {
                return NotFound("Post not found.");
            }
            existingPost.Description = postDTO.Description;
            existingPost.FilePath = postDTO.FilePath;

            await _postRepo.UpdateAsync(existingPost);
            return Ok(existingPost);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var post = await _postRepo.GetAsync(id);
            if (post == null)
            {
                return NotFound("Job not found.");
            }
            await _postRepo.RemoveAsync(post.Id);
            return Ok(post);
        }
    }
}