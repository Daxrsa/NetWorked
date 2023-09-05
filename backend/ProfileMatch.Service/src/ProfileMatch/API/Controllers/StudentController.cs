using Domain.Models;
using Domain.TestDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly ProfileMatchDbContext _context;

        public StudentController(ProfileMatchDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _context.Studentet.ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetId(int id)
        {
            var result = await _context.Studentet.Where(i => i.Id == id).FirstOrDefaultAsync();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Add(ProfesoriStudentiDto dto)
        {
            var result = new Studenti()
            {
                Surname = dto.Surname,
                Name = dto.Name
            };
            _context.Studentet.Add(result);
            _context.SaveChanges();
            return Ok("Added successfully");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var prof = _context.Studentet.Find(id);
            _context.Studentet.Remove(prof);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Edit(int id, ProfesoriStudentiDto dto)
        {
            var existing = await _context.Studentet.FindAsync(id);
            existing.Surname = dto.Surname;
            existing.Name = dto.Name;

            await _context.SaveChangesAsync();
            return Ok("edited successfully");
        }

    }
}
