using AvaTradeApp.Domain.Statics;
using AvaTradeApp.Domain.Entities;
using AvaTradeApp.Application.DTOs;
using AvaTradeApp.Infrastructure.Services.Interfaces;

namespace AvaTradeApp.Infrastructure.Services.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IRefreshTokenService _refreshTokenService;

        public AuthenticationService(
            IUserRepository userManager,
            IJwtTokenGenerator jwtTokenGenerator,
            IRefreshTokenService refreshTokenService)
        {
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
            _refreshTokenService = refreshTokenService;
        }
        public async Task<JwtResponseDto> RegisterAsync(string username, string email, string password)
        {
            var user = new User { UserName = username, Email = email };
            var result = await _userManager.CreateUserAsync(username,email, password);

            if (!result.Succeeded)
            {
                string errorMessage = string.Empty;
                foreach (var error in result.Errors)
                {
                    errorMessage += string.Format("Message : {0}\n", error);
                }
                throw new InvalidOperationException(errorMessage);
            }

            var jwtToken = _jwtTokenGenerator.GenerateJwtToken(user);
            var refreshToken = _refreshTokenService.GenerateRefreshToken();

            return new JwtResponseDto
            {
                JwtToken = jwtToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<JwtResponseDto> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null || !(await _userManager.CheckPasswordAsync(user, password)))
            {
                throw new UnauthorizedAccessException(AccountDetailsinfo.LoginFailedMessage);
            }
            var jwtToken = _jwtTokenGenerator.GenerateJwtToken(user);
            var refreshToken = _refreshTokenService.GenerateRefreshToken();

            return new JwtResponseDto
            {
                JwtToken = jwtToken,
                RefreshToken = refreshToken
            };
        }
        public async Task<User> SubscribeAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new UnauthorizedAccessException(AccountDetailsinfo.SubscribeFailedMessage);
            }
            user.IsSubscribed = true;
            await _userManager.UpdateAsync(user);
            return user;
        }
    }
}
