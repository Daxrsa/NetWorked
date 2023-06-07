using Application.Core;
using Domain.Models;

namespace Application.Services.Auth
{
    public interface IAuthRepo
    {
        public string Username { get; set; }
        Task<Result<Guid>> Register(User user, string password);
        Task<Result<string>> Login(string username, string password);
        Task<bool> UserExists(string username);
        //string GetUserName();
    }
}