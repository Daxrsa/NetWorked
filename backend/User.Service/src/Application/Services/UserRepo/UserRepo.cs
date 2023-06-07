using System.Security.Claims;
using Application.Core;
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
        public UserRepo(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public static Guid GetIdClaim(ClaimsPrincipal claimsPrincipal)
        {
            string idValue = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            Guid id = Guid.Parse(idValue);
            return id;
        }

        public User GetLoggedInUser()
        {
            User result = null;

            if (_httpContextAccessor.HttpContext != null)
            {
                var claimsPrincipal = _httpContextAccessor.HttpContext.User;

                if (claimsPrincipal != null)
                {
                    result = new User
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

        public async Task<Result<User>> GetUserById(Guid id)
        {
            var user = await _context.User.FindAsync(id);
            if (user is null)
            {
                return Result<User>.Failure("User not found.");
            }
            return Result<User>.IsSuccess(user);
        }

        public async Task<Result<List<User>>> GetUsers()
        {
            var users = await _context.User.ToListAsync();
            return Result<List<User>>.IsSuccess(users);
        }

        public async Task<Result<User>> DeleteUser(Guid id)
        {
            var user = await _context.User.FindAsync(id);
            if (user is null)
            {
                return Result<User>.Failure("User not found.");
            }
            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return Result<User>.IsSuccess(user);
        }
    }
}