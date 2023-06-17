using Application.DTOs;
using Domain.Models;
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
        public async Task<ActionResult<List<UserDTO>>> GetAllUsers()
        {
            return Ok(await _userRepo.GetUsers());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(Guid id)
        {
            return Ok(await _userRepo.GetUserById(id));
        }

        [HttpDelete]
        public async Task<ActionResult<UserDTO>> DeleteUser(Guid id)
        {
            return Ok(await _userRepo.DeleteUser(id));
        }

        [HttpGet("countUsers")]
        public async Task<ActionResult<int>> CountUsers()
        {
            return Ok(await _userRepo.GetUserCount());
        }

        [HttpPut]
        public async Task<ActionResult<User>> EditUser(Guid id, EditUserDTO requestDto)
        {
            return Ok(await _userRepo.EditUser(id, requestDto)); 
        }
    }
}