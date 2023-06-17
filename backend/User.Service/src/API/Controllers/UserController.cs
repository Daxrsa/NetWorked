using System.Net.Http.Headers;
using Application.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly HttpClient _httpClient;

        public UserController(IUserRepo userRepo, HttpClient httpClient)
        {
            _userRepo = userRepo;
            _httpClient = httpClient;
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

        [HttpPut("")]
        public async Task<ActionResult<User>> EditUser(Guid id, EditUserDTO requestDto)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString().Split(' ')[1];

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.GetAsync("http://localhost:5116/api/Auth/GetloggedInUser");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var loggedInUser = JsonConvert.DeserializeObject<UserDTO>(content);
                    id = loggedInUser.Id;
                    return Ok(await _userRepo.EditUser(id,requestDto));  
                }

                return BadRequest("Failed to retrieve the logged-in user.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
    }
}