using Application.Core;
using Domain.Models;

namespace Application.Services.UserRepo
{
    public interface IUserRepo
    {
        Task<Result<List<User>>> GetUsers();
        Task<Result<User>> GetUserById(Guid id);
        string GetLoggedInUser();
    }
}