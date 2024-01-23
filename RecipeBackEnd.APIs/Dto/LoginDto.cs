using System.ComponentModel.DataAnnotations;

namespace RecipeBackEnd.APIs.Dto
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }    // MohamedHamed123@Gamil.com

        [Required]
        public string Password { get; set; }
    }
}
