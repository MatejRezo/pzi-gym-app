using SportiverseApi.Models.Auth;

namespace SportiverseApi.Models.User
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public Role UserRole { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
