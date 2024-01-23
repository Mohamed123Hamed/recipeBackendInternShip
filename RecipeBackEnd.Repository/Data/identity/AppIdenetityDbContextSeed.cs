using Microsoft.AspNetCore.Identity;
using RecipeBackEnd.Core.Models.identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBackEnd.Repository.Data.identity
{
    public class AppIdenetityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var User = new AppUser()
                {
                    DisplayName = "Mohamed Hamed",
                    Email = "Mo7hamed123@gmail.com",
                    UserName = "Mo7hamed123",
                    PhoneNumber = "01066622222"
                };
                await userManager.CreateAsync(User, "Pa$$w0rd");
            }
        }
    }
}
