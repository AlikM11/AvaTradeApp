using AvaTradeApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using AvaTradeApp.Infrastructure.Services.Interfaces;

namespace AvaTradeApp.Infrastructure.Services.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> CreateUserAsync(string username, string email, string password)
        {
            var user = new User { UserName = username, Email = email };
            var result = await _userManager.CreateAsync(user, password);
            return result;
        }
        public async Task<User> FindByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user;
        }
        public async Task<bool> CheckPasswordAsync(User user,string password)
        {
            var response = await _userManager.CheckPasswordAsync(user,password);
            return response;
        }
        public async Task<IdentityResult> UpdateAsync(User user)
        {
            var response = await _userManager.UpdateAsync(user);
            return response;
        }
    }
}
