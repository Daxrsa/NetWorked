using Application.Core;
using Application.DTOs;
using Domain.Models;

namespace Application.Services.UserRepo
{
    public interface IUserRepo
    {
        Task<Result<List<UserDTO>>> GetUsers();
        Task<Result<UserDTO>> GetUserById(Guid id);
        Task<Result<UserDTO>> DeleteUser(Guid id);
        UserDTO GetLoggedInUser();
    }
}