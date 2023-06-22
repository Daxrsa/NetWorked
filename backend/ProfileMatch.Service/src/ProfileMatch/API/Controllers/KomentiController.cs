using Domain.Models;
using Domain.TestDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class KomentiController : ControllerBase
    {
        private readonly ProfileMatchDbContext _context;

        public KomentiController(ProfileMatchDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _context.Komentet.ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetId(int id)
        {
            var result = await _context.Komentet.Where(i => i.Id == id).FirstOrDefaultAsync();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Add(AddKomentDto komenti)
        {
            var comment = new Komenti()
            {
                Title= komenti.Title,
                ArticleId= komenti.ArticleId,
                Content= komenti.Content
            };
            _context.Komentet.Add(comment);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var comment = _context.Komentet.Find(id);
            _context.Komentet.Remove(comment);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Edit(int id, AddKomentDto dto)
        {
            var existingComment = await _context.Komentet.FindAsync(id);
            existingComment.Title = dto.Title;
            existingComment.ArticleId = dto.ArticleId;
            existingComment.Content = dto.Content;

            await _context.SaveChangesAsync();
            return Ok("edited successfully");
        }

    }
}
