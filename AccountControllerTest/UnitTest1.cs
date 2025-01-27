using AvaTradeApp.Application.DTOs;
using AvaTradeApp.Infrastructure.Services.Interfaces;
using AvaTradeApp.WebApi.Controllers;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AvaTradeApp.Domain.Entities;
using System.Data.Entity.Core.Objects;

namespace UnitTests
{
    public class AccountControllerTests
    {
        private readonly Mock<IAuthenticationService> _mockAuthService;
        private readonly AccountController _controller;

        public AccountControllerTests()
        {
            _mockAuthService = new Mock<IAuthenticationService>();
            _controller = new AccountController(_mockAuthService.Object);
        }

        [Fact]
        public async Task Register_ReturnsOkResult_WhenRegistrationIsSuccessful()
        {
            // Arrange
            var request = new RegisterRequestDto { Username = "test", Email = "test@example.com", Password = "Password123!" };
            var expectedResponse = new JwtResponseDto { Token = "test_token" };
            _mockAuthService.Setup(service => service.RegisterAsync(request.Username, request.Email, request.Password))
                            .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.Register(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedResponse, okResult.Value);
        }

        [Fact]
        public async Task Login_ReturnsOkResult_WhenLoginIsSuccessful()
        {
            // Arrange
            var request = new LoginRequestDto { Email = "test@example.com", Password = "Password123!" };
            var expectedResponse = new JwtResponseDto { Token = "test_token" };
            _mockAuthService.Setup(service => service.LoginAsync(request.Email, request.Password))
                            .ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.Login(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Login successful", okResult.Value);
        }

        [Fact]
        public async Task Subscribe_ReturnsOkResult_WhenSubscriptionIsSuccessful()
        {
            // Arrange
            var request = new SubscribeRequestDto { Email = "test@example.com" };
            _mockAuthService.Setup(service => service.SubscribeAsync(request.Email))
                            .ReturnsAsync(new User { UserName = "testUser" });

            // Act
            var result = await _controller.Subscribe(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Subscription successful", okResult.Value);
        }
    }
}
