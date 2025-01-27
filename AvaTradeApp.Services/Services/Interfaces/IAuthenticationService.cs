using AvaTradeApp.Domain.Entities;
using AvaTradeApp.Application.DTOs;

namespace AvaTradeApp.Infrastructure.Services.Interfaces
{
    /// <summary>
    /// The IAuthenticationService interface defines methods for user authentication, including login, registration, and subscription management.
    /// </summary>
    public interface IAuthenticationService
    {
        public Task<JwtResponseDto> LoginAsync(string email, string password);
        public Task<JwtResponseDto> RegisterAsync(string username, string email, string password);
        public Task<User> SubscribeAsync(string email);

    }
}
