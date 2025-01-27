namespace AvaTradeApp.Infrastructure.Services.Interfaces
{
    /// <summary>
    /// The IRefreshTokenService interface defines a method for generating a refresh token.
    /// </summary>
    public interface IRefreshTokenService
    {
        string GenerateRefreshToken();
    }
}
