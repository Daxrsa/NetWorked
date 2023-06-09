using Application.Core;
using Domain.Models;

namespace Application.Services.Auth
{
    public interface IAuthRepo
    {
        Task<Result<Guid>> Register(User user, string password);
        Task<Result<string>> Login(string username, string password);
        Task<bool> UserExists(string username);
        void sendEmail(string emailAddress, string token);
        string GenerateVerificationCode();
        Task<string> Verify(string token);
    }
}