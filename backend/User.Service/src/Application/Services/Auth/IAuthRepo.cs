using Application.Core;
using Application.DTOs;
using Domain.Models;

namespace Application.Services.Auth
{
    public interface IAuthRepo
    {
        Task<Result<Guid>> Register(User user, string password);
        Task<Result<string>> Login(string username, string password);
        Task<bool> UserExists(string username);
        void VerifyEmail(string emailAddress);
    }
}