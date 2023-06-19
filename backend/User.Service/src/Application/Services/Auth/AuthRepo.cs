using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using Application.Core;
using Domain.Models;
using File.Package.FileService;
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
        private readonly IFileService _fileService;

        public AuthRepo(DataContext context, IConfiguration config, IFileService fileService)
        {
            _context = context;
            _config = config;
            _fileService = fileService;
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
            else if (user.VerifiedAt == null)
            {
                response.Success = false;
                response.Message = "Not verified.";
            }
            else
            {
                response.Data = CreateToken(user);
                response.UserName = user.Username;
                response.UserRole = user.Role;

            }
            return response;
        }

        public async Task<Result<Guid>> Register(User user, string password)
        {
            var response = new Result<Guid>();
            if (await UserExists(user.Username))
            {
                response.Success = false;
                response.Message = "User already exists";
                return response;
            }

            if(user.formFile!= null)
            {
                var fileResult = _fileService.SaveImage(user.formFile);
                if(fileResult.Item1 == 1)
                {
                    user.ProfilePictureUrl= fileResult.Item2;
                }
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

        public async Task<string> Verify(string token)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.VerificationToken.Equals(token));
            if (user == null)
            {
                return "Invalid token.";
            }

            user.VerifiedAt= DateTime.Now;
            await _context.SaveChangesAsync();
            return "Verified successfully.";
        }

        public void sendEmail(string emailAddress, string token)
        {
            string verificationCode = GenerateVerificationCode();
            MailMessage message = new MailMessage();
            message.From = new MailAddress("networked758@gmail.com"); 
            message.To.Add(emailAddress);
            message.Subject = "Email Verification";
            var url = $"http://localhost:5116/api/Auth/verify?token={token}";
            //message.Body = $"Your verification code is: {verificationCode}";
            message.Body = $"Verify your email by clicking in this url: "+url;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587); 
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new System.Net.NetworkCredential("networked758@gmail.com", "pllvflwxgwjletje");
            smtpClient.EnableSsl = true;

            smtpClient.Send(message);
        }

        public string GenerateVerificationCode()
        {
            Random random = new Random();
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            const int codeLength = 6;

            StringBuilder codeBuilder = new StringBuilder();

            for (int i = 0; i < codeLength; i++)
            {
                int index = random.Next(characters.Length);
                codeBuilder.Append(characters[index]);
            }

            return codeBuilder.ToString();
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role),   //caktimi i rolit
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.MobilePhone, user.Phone),
                new Claim(ClaimTypes.GivenName, user.Fullname),
                new Claim(ClaimTypes.StreetAddress, user.Address),
                new Claim(ClaimTypes.UserData, user.Skills),
                new Claim(ClaimTypes.HomePhone, user.Profession),
                new Claim(ClaimTypes.CookiePath, user.Bio)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}