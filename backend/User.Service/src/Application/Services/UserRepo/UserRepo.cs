using System.Security.Claims;
using Application.Core;
using Application.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Services.UserRepo
{
    public class UserRepo : IUserRepo, IChangeRole
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
                        Role = claimsPrincipal.FindFirstValue(ClaimTypes.Role),
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
            try
            {
                var user = await _context.User.FindAsync(id);
                if (user is null)
                {
                    return Result<UserDTO>.Failure($"User with id {id} not found.");
                }
                var userDto = _mapper.Map<UserDTO>(user);
                return Result<UserDTO>.IsSuccess(userDto);
            }
            catch (Exception ex)
            {
                return Result<UserDTO>.Failure(ex.Message);
            }
        }

        public async Task<Result<List<UserDTO>>> GetUsers()
        {
            try
            {
                var users = await _context.User.ToListAsync();
                if (users is null)
                {
                    return Result<List<UserDTO>>.Failure("No users found.");
                }
                var userDtos = _mapper.Map<List<UserDTO>>(users);
                return Result<List<UserDTO>>.IsSuccess(userDtos);
            }
            catch (Exception ex)
            {
                return Result<List<UserDTO>>.Failure(ex.Message);
            }
        }
        public async Task UpdateUser(User user)
        {
            var existingUser = await _context.User.FindAsync(user.Id);

            if (existingUser != null)
            {
                existingUser.Role = user.Role;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Result<UserDTO>> DeleteUser(Guid id)
        {
            try
            {
                var user = await _context.User.FindAsync(id);
                if (user is null)
                {
                    return Result<UserDTO>.Failure($"User with id {id} not found.");
                }
                _context.User.Remove(user);

                var result = await _context.SaveChangesAsync() > 0;
                if (!result)
                {
                    return Result<UserDTO>.Failure("Failed to remove the user.");
                }

                var userDto = _mapper.Map<UserDTO>(user);
                return Result<UserDTO>.IsSuccess(userDto);
            }
            catch (Exception ex)
            {
                return Result<UserDTO>.Failure(ex.Message);
            }
        }

        public async Task<Result<List<EditUserDTO>>> EditUser(Guid id, EditUserDTO requestDto)
        {
            try
            {
                var user = await _context.User.FindAsync(id);
                if (user is null)
                {
                    return Result<List<EditUserDTO>>.Failure($"User with id {id} not found.");
                }

                _mapper.Map(requestDto, user);

                var result = await _context.SaveChangesAsync() > 0;
                if (!result)
                {
                    return Result<List<EditUserDTO>>.Failure("Failed to update the user.");
                }
                var users = await _context.User.ProjectTo<EditUserDTO>(_mapper.ConfigurationProvider).ToListAsync();
                return Result<List<EditUserDTO>>.IsSuccess(users);
            }
            catch (Exception ex)
            {
                return Result<List<EditUserDTO>>.Failure(ex.Message);
            }
        }

        public async Task<bool> ChangeUserRole(Guid id)
        {
            var result = await GetUserById(id);

            if (result == null || !result.Success || result.Data == null)
            {
                return false;
            }

            if (result.Data.Role != "Applicant")
            {
                return false;
            }

            result.Data.Role = "Recruiter";

            var user = _mapper.Map<User>(result.Data);
            await UpdateUser(user);
            return true;
        }
    }
}