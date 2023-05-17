using System.Net;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Application.Core;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Persistence;

namespace Application.Services.Auth
{
    public class AuthRepo : IAuthRepo
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;
        public AuthRepo(DataContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<Result<string>> Login(string username, string password)
        {
            var response = new Result<string>();
            var user = await _context.User
                .FirstOrDefaultAsync(u => u.Username.ToLower().Equals(username.ToLower()));
            if (user is null)
            {
                response.Success = false;
                response.Message = "User not found.";
            }
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong password.";
            }
            else
            {
                response.Data = CreateToken(user);

                var refreshToke = GenerateRefreshToken();
                //SetRefreshToken(refreshToke);
            }
            return response;
        }

        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };
            return refreshToken;
        }

        // private void SetRefreshToken(RefreshToken newRefreshToken)
        // {
        //     var cookieOptions = new CookieOptions
        //     {
        //         HttpOnly = true,
        //         Expires = newRefreshToken.Expires
        //     };
        //     Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

        //     user.RefreshToken = newRefreshToken.Token;
        //     user.TokenCreated = newRefreshToken.Created;
        //     user.TokenExpires = newRefreshToken.Expires;
        // }

        public async Task<Result<Guid>> Register(User user, string password)
        {
            var response = new Result<Guid>();
            if (await UserExists(user.Username))
            {
                response.Success = false;
                response.Message = "User already exists";
                return response;
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.User.Add(user);
            await _context.SaveChangesAsync();
            response.Data = user.Id;
            return response;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.User.AnyAsync(u => u.Username.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            // var claims = new List<Claim>
            // {
            //     //new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            //     new Claim(ClaimTypes.Name, user.Username)
            // };
            // var appsettingsToken = _config.GetSection("AppSettings:Token").Value;
            // if (appsettingsToken is null)
            //     throw new Exception("AppSettings Token is null.");
            
            // SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
            //     .GetBytes(appsettingsToken));
            
            // SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // var tokenDescriptor = new SecurityTokenDescriptor
            // {
            //     Subject = new ClaimsIdentity(claims),
            //     Expires = DateTime.Now.AddDays(1),
            //     SigningCredentials = creds
            // };

            // JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            // SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            // return tokenHandler.WriteToken(token);

            //-------------------------------------------------------------------------------------------------------------------------------------------------------
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "Admin"),

            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}