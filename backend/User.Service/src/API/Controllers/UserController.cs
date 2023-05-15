<<<<<<< Updated upstream
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
=======
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    [Authorize]
>>>>>>> Stashed changes
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
<<<<<<< Updated upstream
        
=======
        private readonly DataContext _context;
        public UserController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
>>>>>>> Stashed changes
    }
}