using Application.Services.BlogService;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blog;
        public BlogController(IBlogService blog)
        {
            _blog = blog;
        }

        [HttpGet]
        public async Task<ActionResult<List<Blog>>> GetBlogs()
        {
            return Ok(await _blog.GetBlogs());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Blog>> GetJobById(Guid id)
        {
            return Ok(await _blog.GetSingleBlog(id));
        }

        [HttpPost("add")]
        public async Task<ActionResult<List<Blog>>> AddExam(Blog blog)
        {
            return Ok(await _blog.AddBlog(blog));
        }

        [HttpPut("{id}/edit")]
        public async Task<ActionResult<List<Blog>>> UpdateExam(Guid id, Blog blog)
        {
            return Ok(await _blog.EditBlog(id, blog));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Blog>>> DeleteExam(Guid id)
        {
            return Ok(await _blog.DeleteBlog(id));
        }
    }
}