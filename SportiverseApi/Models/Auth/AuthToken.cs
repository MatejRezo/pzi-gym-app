namespace SportiverseApi.Models.Auth
{
    public class AuthToken
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Expires { get; set; } = DateTime.Now.AddHours(1);
    }
}
