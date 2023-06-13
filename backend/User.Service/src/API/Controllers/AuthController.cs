using Application.Core;
using Application.DTOs;
using Application.Services.Auth;
using Application.Services.UserRepo;
using Azure;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepo _authRepo;
        private readonly IUserRepo _userRepo;
        public AuthController(IAuthRepo authRepo, IUserRepo userRepo)
        {
            _authRepo = authRepo;
            _userRepo = userRepo;
        }

        [HttpGet("GetloggedInUser"), Authorize]
        public ActionResult<string> GetLoggedInUser()
        {
            var user = _userRepo.GetLoggedInUser();
            return Ok(user);
        }
        [HttpPost("Register")]
        public async Task<ActionResult<Result<Guid>>> Register([FromForm] UserRegisterDTO request)
        {
            var response = await _authRepo.Register(
                new User
                {
                    Username = request.Username,
                    Fullname = request.Fullname,
                    Email = request.Email,
                    Phone = request.Phone,
                    Address = request.Address,
                    Profession = request.Profession,
                    Skills = request.Skills,
                    Bio = request.Bio,
                    Role = "Applicant",
                    formFile = request.formFile
                },
                request.Password
            ); ;
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        // [HttpPost("ChangeUserRole")]
        // //[Authorize(Roles = "Admin")] // Add appropriate authorization for this endpoint
        // public async Task<ActionResult<string>> ChangeUserRole(Guid id)
        // {
        //     var result = await _userRepo.GetUserById(id);

        //     if (result == null || !result.Success || result.Data == null)
        //     {
        //         return NotFound();
        //     }

        //     if (result.Data.Role != "Applicant")
        //     {
        //         return BadRequest("User role is not 'Applicant'.");
        //     }

        //     result.Data.Role = "Recruiter";

        //     // Update the user role in the repository
        //     await _userRepo.UpdateUser(result.Data);

        //     return Ok(result);
        // }


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