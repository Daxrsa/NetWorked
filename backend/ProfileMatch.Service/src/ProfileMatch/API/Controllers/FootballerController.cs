using Domain.Models;
using Domain.TestDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class FootballerController : ControllerBase
    {
        private readonly ProfileMatchDbContext _context;

        public FootballerController(ProfileMatchDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _context.Footballers.ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetId(int id)
        {
            var result = await _context.Footballers.Where(i => i.Id == id).FirstOrDefaultAsync();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Add(AddFootballerDto komenti)
        {
            var comment = new Footballer()
            {
                Name= komenti.Name,
                Surname= komenti.Surname,
                TeamId= komenti.TeamId
            };
            _context.Footballers.Add(comment);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var comment = _context.Footballers.Find(id);
            _context.Footballers.Remove(comment);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Edit(int id, AddFootballerDto dto)
        {
            var existing = await _context.Footballers.FindAsync(id);
            existing.Name = dto.Name;
            existing.Surname = dto.Surname;
            existing.TeamId = dto.TeamId;

            await _context.SaveChangesAsync();
            return Ok("edited successfully");
        }

    }
}
