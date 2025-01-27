using AvaTradeApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace AvaTradeApp.Infrastructure.Services.Interfaces
{
    public interface IUserRepository
    {
        Task<User> FindByEmailAsync(string email);
        Task<IdentityResult> UpdateAsync(User user);
        Task<bool> CheckPasswordAsync(User user, string password);
        Task<IdentityResult> CreateUserAsync(string username, string email, string password);
    }
}
