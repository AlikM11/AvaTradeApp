using AvaTradeApp.Domain.Entities;

namespace AvaTradeApp.Infrastructure.Services.Interfaces
{
    /// <summary>
    /// The IJwtTokenGenerator interface defines a method for generating a JWT token for a given user.
    /// </summary>
    public interface IJwtTokenGenerator
    {
        string GenerateJwtToken(User user);
    }
}
