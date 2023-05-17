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

        public string GetLoggedInUser()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
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
    }
}