namespace RecipeBackEnd.APIs.Dto
{
    // Dto => Data Transfer Object --> responsible for data returned[Relation between front , back]
    public class UserDto
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
