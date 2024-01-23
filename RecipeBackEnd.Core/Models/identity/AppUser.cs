using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBackEnd.Core.Models.identity
{
    public class AppUser : IdentityUser
    {
        public String DisplayName { get; set; }
        public Address Address { get; set; }      //Navigational Property [one]
    }
 
}
