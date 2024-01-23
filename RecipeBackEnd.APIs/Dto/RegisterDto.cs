using System.ComponentModel.DataAnnotations;

namespace RecipeBackEnd.APIs.Dto
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&amp;*().+]).*$",
         ErrorMessage = "Password Must Contain 1 UpperCase,1 LowerCase,1 Digit,1 Special Characters")]
        public string Password { get; set; }
    }
}
