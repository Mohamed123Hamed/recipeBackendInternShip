using RecipeBackEnd.Core.Models.identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBackEnd.Core.Service
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(AppUser user);
    }
}
