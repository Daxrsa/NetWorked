using Application.Core;
using Application.DTOs;

namespace Application.Services.UserRepo
{
    public interface IUserRepo
    {
        Task<Result<List<UserDTO>>> GetUsers();
        Task<Result<UserDTO>> GetUserById(Guid id);
        Task<Result<UserDTO>> DeleteUser(Guid id);
        Task<Result<List<EditUserDTO>>> EditUser(Guid id, EditUserDTO requestDto);
        UserDTO GetLoggedInUser();
    }
}