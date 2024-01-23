namespace RecipeBackEnd.Core.Models.identity
{
    public class Address
    {
        public int Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string AppUserId { get; set; }   // FK => string because inhert idenetity is id in idenetity is Guid and inhert Type is string
        public AppUser User { get; set; } // Navigational Property [one]
    }
}