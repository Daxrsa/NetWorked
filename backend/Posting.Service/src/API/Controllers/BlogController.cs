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

        [HttpPost("addContentToBlog")]
        public async Task<ActionResult<Blog>> AddContentToBlog(Guid blogId, Content newContent)
        {
            return Ok(await _blog.AddContentToBlog(blogId, newContent));
        }

        [HttpDelete("{id}/deletecontent")]
        public async Task<ActionResult<Content>> DeleteContent(Guid id)
        {
            return Ok(await _blog.DeleteContent(id));
        }

        [HttpPut("{id}/editContentFromBlog")]
        public async Task<ActionResult<List<Blog>>> EditContent(Guid id, string newBody)
        {
            return Ok(await _blog.EditContent(id, newBody));
        }

        [HttpGet("GetAllContent")]
        public async Task<ActionResult<List<Content>>> GetAllContent()
        {
            return Ok(await _blog.GetAllContent());
        }

        [HttpGet("{id}/getContentbyId")]
        public async Task<ActionResult<Blog>> GetContentById(Guid id)
        {
            return Ok(await _blog.GetContentById(id));
        }
    }
}