using Application.Services.UserRepo;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    //[Authorize(Roles = "Admin")]
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

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(Guid id)
        {
            return Ok(await _userRepo.GetUserById(id));
        }

        [HttpDelete]
        public async Task<ActionResult<User>> DeleteUser(Guid id)
        {
            return Ok(await _userRepo.DeleteUser(id));
        }

        [HttpGet("Logged")]
        public ActionResult<User> GetLoggedInUser() 
        {
            var userId = _userRepo.GetUserId();
            var isAuth = _userRepo.IsUserAuthenticated();
            if (isAuth)
            {
                return Ok(_userRepo.GetUserById(userId));
            }
            return Ok(null);
        }
    }
}