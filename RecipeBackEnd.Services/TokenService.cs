using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RecipeBackEnd.Core.Models.identity;
using RecipeBackEnd.Core.Service;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBackEnd.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> CreateTokenAsync(AppUser user)
        {
            // PayLoad [Data] [claim]
            // 1. Private Claim
           var AuthClaim = new List<Claim>()
           {
            new Claim(ClaimTypes.GivenName , user.DisplayName),
            new Claim (ClaimTypes.Email , user.Email)
           };

            // 2. Register Claims [issuer,audience,expires]

            // 3. Auth key
            var AuthKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));// covert string to array of bite
            var Token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssure"],            // Register claims
                audience: _configuration["JWT:ValidAudience"],        // Register claims
                expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:DurationInDay"])), // Register claims
                claims: AuthClaim,         // private claims
                signingCredentials: new SigningCredentials(AuthKey, SecurityAlgorithms.HmacSha256));  //encryption [key , header] 
           return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
