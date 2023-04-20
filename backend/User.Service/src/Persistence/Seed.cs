using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context, UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        DisplayName = "Daorsa Hyseni",
                        UserName = "daxrsa",
                        Email = "daorsahyseni@gmail.com"
                    },

                    new AppUser
                    {
                        DisplayName = "John Doe",
                        UserName = "johndoe",
                        Email = "johndoe@gmail.com"
                    },

                    new AppUser
                    {
                        DisplayName = "Johnny Parker",
                        UserName = "jinx",
                        Email = "johnnyparker@gmail.com"
                    },
                };

                foreach (var user in users)
                {
                    var result = await userManager.CreateAsync(user, "Password123@");
                    if (!result.Succeeded)
                    {
                        throw new Exception($"Error seeding user {user.UserName}. Error: {result.Errors.FirstOrDefault()?.Description}");
                    }
                }
            }
        }
    }
}