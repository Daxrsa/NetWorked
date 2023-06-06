using System.Security.Claims;
using Application.Core;
using Application.DTOs;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services.UserRepo
{
    public class UserRepo : IUserRepo
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public UserRepo(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public static Guid GetIdClaim(ClaimsPrincipal claimsPrincipal)
        {
            string idValue = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            Guid id = Guid.Parse(idValue);
            return id;
        }

        public UserDTO GetLoggedInUser()
        {
            UserDTO result = null;

            if (_httpContextAccessor.HttpContext != null)
            {
                var claimsPrincipal = _httpContextAccessor.HttpContext.User;

                if (claimsPrincipal != null)
                {
                    result = new UserDTO
                    {
                        Username = claimsPrincipal.FindFirstValue(ClaimTypes.Name),
                        Email = claimsPrincipal.FindFirstValue(ClaimTypes.Email),
                        //Role = claimsPrincipal.FindFirstValue(ClaimTypes.Role),
                        Id = GetIdClaim(claimsPrincipal),
                        Phone = claimsPrincipal.FindFirstValue(ClaimTypes.MobilePhone),
                        Fullname = claimsPrincipal.FindFirstValue(ClaimTypes.GivenName),
                        Address = claimsPrincipal.FindFirstValue(ClaimTypes.StreetAddress),
                        Skills = claimsPrincipal.FindFirstValue(ClaimTypes.UserData),
                        Profession = claimsPrincipal.FindFirstValue(ClaimTypes.HomePhone),
                        Bio = claimsPrincipal.FindFirstValue(ClaimTypes.CookiePath)
                    };
                }
            }

            return result;
        }

        public async Task<Result<UserDTO>> GetUserById(Guid id)
        {
            var user = await _context.User.FindAsync(id);
            if (user is null)
            {
                return Result<UserDTO>.Failure("User not found.");
            }
            var userDto = _mapper.Map<UserDTO>(user);
            return Result<UserDTO>.IsSuccess(userDto);
        }

        public async Task<Result<List<UserDTO>>> GetUsers()
        {
            var users = await _context.User.ToListAsync();
            var userDtos = _mapper.Map<List<UserDTO>>(users);
            return Result<List<UserDTO>>.IsSuccess(userDtos);
        }

        public async Task<Result<UserDTO>> DeleteUser(Guid id)
        {
            var user = await _context.User.FindAsync(id);
            if (user is null)
            {
                return Result<UserDTO>.Failure("User not found.");
            }
            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            var userDto = _mapper.Map<UserDTO>(user);
            return Result<UserDTO>.IsSuccess(userDto);
        }
    }
}