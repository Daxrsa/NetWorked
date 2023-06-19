using API.RabbitMQConfig;
using Application.Core;
using Application.DTOs;
using AutoMapper;
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
        private readonly IMessageProducer _messageProducer;
        private readonly IMapper _mapper;
        public AuthController(IAuthRepo authRepo, IUserRepo userRepo, IMessageProducer messageProducer, IMapper mapper)
        {
            _authRepo = authRepo;
            _userRepo = userRepo;
            _messageProducer = messageProducer;
            _mapper = mapper;
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
            var user = new User()
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
                formFile = request.formFile,
                VerificationToken = _authRepo.GenerateVerificationCode()
            };
            var response = await _authRepo.Register(user, request.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            _authRepo.sendEmail(user.Email, user.VerificationToken);
            return Ok(response);
        }

        [HttpPost("ChangeUserRole")]
        public async Task<ActionResult<string>> ChangeUserRole(Guid id)
        {
            var result = await _userRepo.GetUserById(id);

            if (result == null || !result.Success || result.Data == null)
            {
                return NotFound();
            }

            if (result.Data.Role != "Applicant")
            {
                return BadRequest("User role is not 'Applicant'.");
            }

            result.Data.Role = "Recruiter";
            var user = _mapper.Map<User>(result.Data);
            await _userRepo.UpdateUser(user);

            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<Result<Guid>>> Login(UserLoginDTO request)
        {
            var response = await _authRepo.Login(request.Username, request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            var user = new DTO { Username = response.UserName };
            _messageProducer.SendMessage<DTO>(user, "user_service");

            return Ok(response);
        }

        //URL: http://localhost:5116/api/Auth/verify?token=token
        [HttpGet("verify")]
        public async Task<IActionResult> Verfiy(string token)
        {
            var result = await _authRepo.Verify(token);
            return Ok(result);
        }
    }
}