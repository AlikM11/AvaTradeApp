using Microsoft.AspNetCore.Mvc;
using AvaTradeApp.Domain.Statics;
using AvaTradeApp.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using AvaTradeApp.Infrastructure.Services.Interfaces;

namespace AvaTradeApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Registers a new user.
        /// The method expects a JSON payload with the user's registration details (username, email, and password).
        /// If the registration is successful, a response is returned with the user's details.
        /// If an error occurs during registration, a BadRequest response with the error message is returned.
        /// </summary>
        /// <param name="request">The registration request containing the user's username, email, and password.</param>
        /// <returns>
        /// Returns an IActionResult with a 200 OK status and the response object on success.
        /// If registration fails, a 400 BadRequest status is returned with an error message.
        /// </returns>

        [HttpPost("RegisterAsync")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequestDto request)
        {
            try
            {
                var response = await _authenticationService.RegisterAsync(request.Username, request.Email, request.Password);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Authenticates a user and returns a response based on login success or failure.
        /// The method expects a JSON payload with the user's email and password for authentication.
        /// If the login is successful, a success message is returned.
        /// If authentication fails, a failure message is returned.
        /// </summary>
        /// <param name="request">The login request containing the user's email and password.</param>
        /// <returns>
        /// Returns a 200 OK status with a success or failure message depending on the authentication result.
        /// If an error occurs during authentication, a 401 Unauthorized status is returned with an error message.
        /// </returns>

        [HttpPost("LoginAsync")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDto request)
        {
            try
            {
                var response = await _authenticationService.LoginAsync(request.Email, request.Password);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Subscribes a user based on their email address.
        /// The method expects a JSON payload with the user's email for subscription.
        /// If the subscription is successful, a success message is returned.
        /// If the subscription fails, an unauthorized response with a failure message is returned.
        /// </summary>
        /// <param name="request">The subscription request containing the user's email address.</param>
        /// <returns>
        /// Returns a 200 OK status with a success message if the subscription is successful.
        /// If the subscription fails, a 401 Unauthorized status is returned with a failure message.
        /// If an error occurs, a 401 Unauthorized status is returned with the error message.
        /// </returns>

        [Authorize]
        [HttpPost("SubscribeAsync")]
        public async Task<IActionResult> SubscribeAsync([FromBody] SubscribeRequestDto request)
        {
            try
            {
                var response = await _authenticationService.SubscribeAsync(request.Email);
                if(response != null)
                {
                    return Ok(AccountDetailsinfo.SubscribeSuccessMessage);
                }
                else
                {
                    return Unauthorized(AccountDetailsinfo.SubscribeFailedMessage);
                }
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
    }
}
