using Domain.Models;
using Domain.TestDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SemundjaController : ControllerBase
    {
        private readonly ProfileMatchDbContext _context;

        public SemundjaController(ProfileMatchDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _context.Semundjet.ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetId(int id)
        {
            var result = await _context.Semundjet.Where(i => i.Id == id).FirstOrDefaultAsync();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Add(AddSemundjaDto komenti)
        {
            var comment = new Semundja()
            {
                Name = komenti.Name,
                SpecializimiId = komenti.SpecializimiId
            };
            _context.Semundjet.Add(comment);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var comment = _context.Semundjet.Find(id);
            _context.Semundjet.Remove(comment);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Edit(int id, AddSemundjaDto dto)
        {
            var existingComment = await _context.Semundjet.FindAsync(id);
            existingComment.Name = dto.Name;
            existingComment.SpecializimiId = dto.SpecializimiId;

            await _context.SaveChangesAsync();
            return Ok("edited successfully");
        }

    }
}
