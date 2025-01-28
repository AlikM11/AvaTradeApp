using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AvaTradeApp.Infrastructure.Services.Interfaces;

namespace AvaTradeApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        /// <summary>
        /// Retrieves all news items by calling the service layer -> [Authorize] Get all news. 
        /// The action is protected by the [Authorize] attribute, meaning the request requires authentication.
        /// </summary>
        /// <returns>
        /// An IActionResult containing a list of news items in the response body.
        /// If successful, returns an HTTP 200 OK status with the news list.
        /// </returns>
        
        [Authorize]
        [HttpGet("GetAllNewsAsync")]
        public async Task<IActionResult> GetAllNewsAsync()
        {
            var news = await _newsService.GetAllNewsAsync();
            return Ok(news);
        }

        /// <summary>
        /// Retrieves news items from the service layer that were published within a given number of days -> [Authorize] Get all news from today – {n} days.
        /// The action is protected by the [Authorize] attribute, meaning the request requires authentication.
        /// </summary>
        /// <param name="day">The number of days to filter the news by their publication date.</param>
        /// <returns>
        /// An IActionResult containing a list of news items published within the specified number of days.
        /// If successful, returns an HTTP 200 OK status with the filtered news list.
        /// </returns>

        [Authorize]
        [HttpGet("GetAllNewsWithGivingDayAsync")]
        public async Task<IActionResult> GetAllNewsWithGivingDayAsync([FromQuery]int day)
        {
            var news = await _newsService.GetAllNewsWithGivingDayAsync(day);
            return Ok(news);
        }

        /// <summary>
        /// Retrieves a list of news articles based on a specified keyword, limited to a specified number of results -> [Authorize] Get all news per instrument name include news limit (default limit = 10).
        /// The action is secured with the [Authorize] attribute, ensuring that only authenticated users can access the endpoint.
        /// </summary>
        /// <param name="keyword">The keyword to filter news articles by, returning only those that match the provided keyword.</param>
        /// <param name="limit">The limit to getting value with limit.</param>
        /// <returns>
        /// Returns an IActionResult with a status of 200 OK and a list of news items that match the given keyword and limit.
        /// If no news articles are found, an empty list is returned.
        /// </returns>

        [Authorize]
        [HttpGet("GetAllNewsPerInstrumentWithLimitAsync")]
        public async Task<IActionResult> GetAllNewsPerInstrumentWithLimitAsync([FromQuery] string keyword, [FromQuery] int limit)
        {
            var news = await _newsService.GetAllNewsPerInstrumentWithLimitAsync(keyword, limit);
            return Ok(news);
        }

        /// <summary>
        /// Retrieves a list of news articles filtered by a specified keyword -> [Authorize] Get all news that contains {text}..
        /// The action is secured with the [Authorize] attribute, ensuring that only authenticated users can access the endpoint.
        /// </summary>
        /// <param name="keyword">The keyword to filter news articles by, returning only those that match the provided keyword.</param>
        /// <returns>
        /// Returns an IActionResult with a status of 200 OK and a list of news items that match the given keyword.
        /// If no news articles are found, an empty list is returned.
        /// </returns>

        [Authorize]
        [HttpGet("GetAllNewsPerInstrumentAsync")]
        public async Task<IActionResult> GetAllNewsPerInstrumentAsync([FromQuery] string keyword)
        {
            var news = await _newsService.GetAllNewsPerInstrumentAsync(keyword);
            return Ok(news);
        }

        /// <summary>
        /// Retrieves the latest news articles -> [Public] Get latest new (top latest 5 different instruments) for conversion tool..
        /// The action is secured with the [Authorize] attribute, ensuring that only authenticated users can access the endpoint.
        /// </summary>
        /// <returns>
        /// Returns an IActionResult with a status of 200 OK and a list of the latest news articles.
        /// If no news articles are found, an empty list is returned.
        /// </returns>

        [HttpGet("GetLatestNewsAsync")]
        public async Task<IActionResult> GetLatestNewsAsync()
        {
            var news = await _newsService.GetLatestNewsAsync();
            return Ok(news);
        }
    }
}
