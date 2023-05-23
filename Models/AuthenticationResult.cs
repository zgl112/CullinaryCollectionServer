namespace baseAPI.Models
{
    public class AuthenticationResult
    {
        public string Token { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public User User { get; set; }
    }
}
