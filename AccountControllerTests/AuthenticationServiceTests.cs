using Moq;
using AvaTradeApp.Domain.Entities;
using AvaTradeApp.Application.DTOs;
using Microsoft.AspNetCore.Identity;
using AvaTradeApp.Infrastructure.Services.Interfaces;
using AvaTradeApp.Infrastructure.Services.Implementation;

namespace AccountControllerTests
{
    public class AuthenticationServiceTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IJwtTokenGenerator> _mockJwtTokenGenerator;
        private readonly Mock<IRefreshTokenService> _mockRefreshTokenService;
        private readonly IAuthenticationService _authService;

        public AuthenticationServiceTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockJwtTokenGenerator = new Mock<IJwtTokenGenerator>();
            _authService = new AuthenticationService(_mockUserRepository.Object, _mockJwtTokenGenerator.Object, _mockRefreshTokenService.Object);
        }

        [Fact]
        public async Task RegisterAsync_ShouldReturnJwtResponse_WhenRegistrationIsSuccessful()
        {
            var request = new RegisterRequestDto { Username = "test", Email = "test@example.com", Password = "Password123!" };
            var user = new User { UserName = request.Username, Email = request.Email };

            _mockUserRepository.Setup(repo => repo.CreateUserAsync(request.Username, request.Email, request.Password))
                .ReturnsAsync(IdentityResult.Success);

            _mockJwtTokenGenerator.Setup(jwt => jwt.GenerateJwtToken(It.IsAny<User>()))
                .Returns("test_jwt_token");

            var result = await _authService.RegisterAsync(request.Username, request.Email, request.Password);

            Assert.NotNull(result);
            Assert.Equal("test_jwt_token", result.JwtToken);
        }

        [Fact]
        public async Task RegisterAsync_ShouldThrowException_WhenRegistrationFails()
        {
            var request = new RegisterRequestDto { Username = "test", Email = "test@example.com", Password = "Password123!" };

            _mockUserRepository.Setup(repo => repo.CreateUserAsync(request.Username, request.Email, request.Password))
                .ReturnsAsync(IdentityResult.Failed());

            await Assert.ThrowsAsync<Exception>(() => _authService.RegisterAsync(request.Username, request.Email, request.Password));
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnJwtResponse_WhenLoginIsSuccessful()
        {
            var request = new LoginRequestDto { Email = "test@example.com", Password = "Password123!" };
            var user = new User { UserName = "test", Email = request.Email };

            _mockUserRepository.Setup(repo => repo.FindByEmailAsync(request.Email)).ReturnsAsync(user);
            _mockUserRepository.Setup(repo => repo.CheckPasswordAsync(user, request.Password)).ReturnsAsync(true);
            _mockJwtTokenGenerator.Setup(jwt => jwt.GenerateJwtToken(user)).Returns("test_jwt_token");

            var result = await _authService.LoginAsync(request.Email, request.Password);

            Assert.NotNull(result);
            Assert.Equal("test_jwt_token", result.JwtToken);
        }

        [Fact]
        public async Task LoginAsync_ShouldThrowException_WhenUserNotFound()
        {
            var request = new LoginRequestDto { Email = "nonexistent@example.com", Password = "Password123!" };

            _mockUserRepository.Setup(repo => repo.FindByEmailAsync(request.Email)).ReturnsAsync((User)null);

            await Assert.ThrowsAsync<Exception>(() => _authService.LoginAsync(request.Email, request.Password));
        }

        [Fact]
        public async Task LoginAsync_ShouldThrowException_WhenPasswordIsIncorrect()
        {
            var request = new LoginRequestDto { Email = "test@example.com", Password = "WrongPassword123!" };
            var user = new User { UserName = "test", Email = request.Email };

            _mockUserRepository.Setup(repo => repo.FindByEmailAsync(request.Email)).ReturnsAsync(user);
            _mockUserRepository.Setup(repo => repo.CheckPasswordAsync(user, request.Password)).ReturnsAsync(false);

            await Assert.ThrowsAsync<Exception>(() => _authService.LoginAsync(request.Email, request.Password));
        }
    }
}