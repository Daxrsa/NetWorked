using Application.Services.UserRepo;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        public UserController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            return Ok(await _userRepo.GetUsers());
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(Guid id)
        {
            return Ok(await _userRepo.GetUserById(id));
        }
    }
}