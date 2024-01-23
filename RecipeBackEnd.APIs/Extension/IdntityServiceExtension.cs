using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RecipeBackEnd.Core.Models.identity;
using RecipeBackEnd.Core.Service;
using RecipeBackEnd.Repository.Data.identity;
using RecipeBackEnd.Services;
using System.Text;

namespace RecipeBackEnd.APIs.Extension
{
    public static class IdntityServiceExtension
    {
        // Add  User
        public static IServiceCollection AddIdentityServices(this IServiceCollection services,
                                                                  IConfiguration configuration)
        {
           services.AddIdentity<AppUser, IdentityRole>()
                   .AddEntityFrameworkStores<AppIdentityDbContext>();

           services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(options =>
                     {
                        options.TokenValidationParameters = new TokenValidationParameters()
                        { 
                        ValidateIssuer = true,
                        ValidIssuer = configuration["JWT:ValidIssure"],
                        ValidateAudience = true,
                        ValidAudience = configuration["JWT:ValidAudience"],
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey
                                           (Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
                        };
                     });

           services.AddScoped<ITokenService, TokenService>();

           return services;
        }
    }
}
