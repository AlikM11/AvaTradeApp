using Microsoft.AspNetCore.Identity;

namespace AvaTradeApp.Domain.Entities
{
    public class User : IdentityUser
    {
        public bool IsSubscribed { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<RefreshToken>? RefreshTokens { get; set; }
    }
}
