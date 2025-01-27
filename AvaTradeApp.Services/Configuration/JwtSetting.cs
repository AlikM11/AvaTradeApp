namespace AvaTradeApp.Infrastructure.Configuration
{
    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretKey { get; set; }
        public string Subject { get; set; }
        public int TokenExpirationMinutes { get; set; }
    }
}
