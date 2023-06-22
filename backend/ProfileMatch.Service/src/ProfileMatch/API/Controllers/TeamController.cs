using Domain.Models;
using Domain.TestDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly ProfileMatchDbContext _context;

        public TeamController(ProfileMatchDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _context.Teams.ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetId(int id)
        {
            var result = await _context.Teams.Where(i => i.Id == id).FirstOrDefaultAsync();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Add(AddTeamDto dto)
        {
            var team = new Team()
            {
                Name = dto.Name
            };
            _context.Teams.Add(team);
            _context.SaveChanges();
            return Ok("Added successfully");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var team = _context.Teams.Find(id);
            _context.Teams.Remove(team);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Edit(int id, AddTeamDto dto)
        {
            var existing = await _context.Teams.FindAsync(id);
            existing.Name = dto.Name;

            await _context.SaveChangesAsync();
            return Ok("edited successfully");
        }

    }
}
