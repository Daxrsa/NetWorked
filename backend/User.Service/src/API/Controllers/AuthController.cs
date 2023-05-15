using Application.Core;
using Application.DTOs;
using Application.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepo _authRepo;
        public AuthController(IAuthRepo authRepo)
        {
            _authRepo = authRepo;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<Result<Guid>>> Register(UserRegisterDTO request)
        {
            var response = await _authRepo.Register(
                new User { Username = request.Username}, request.Password
            );
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<Result<Guid>>> Login(UserLoginDTO request)
        {
            var response = await _authRepo.Login(request.Username, request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        } 
    }
}