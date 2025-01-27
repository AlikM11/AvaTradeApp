using System.Text;
using System.Text.Json;
using AvaTradeApp.Services;
using System.Net.Http.Headers;
using AvaTradeApp.Domain.Statics;
using AvaTradeApp.Domain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AvaTradeApp.Infrastructure.Services.Interfaces;

namespace AvaTradeApp.Infrastructure.Services.Implementation
{
    public class NewsBackgroundService : BackgroundService
    {
        private readonly ILogger<NewsBackgroundService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IHttpClientService _httpClientService;

        public NewsBackgroundService(ILogger<NewsBackgroundService> logger,
                                     IConfiguration configuration,
                                     IServiceScopeFactory serviceScopeFactory,
                                     IHttpClientService httpClientService)
        {
            _logger = logger;
            _configuration = configuration;
            _serviceScopeFactory = serviceScopeFactory;
            _httpClientService = httpClientService;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            string? apiUrl = _configuration["PolygonApi:NewsUrl"];
            string? apiKey = _configuration["PolygonApi:BearerToken"];
            string? fromEmail = _configuration["Email:FromEmail"];
            string? fromPassword = _configuration["Email:FromPassword"];
            using (HttpClient client = _httpClientService.GetHttpClient())
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation("NewsBackgroundService running at: {time}", DateTime.Now);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                    using (HttpResponseMessage response = await _httpClientService.GetAsync(apiUrl, stoppingToken))
                    {
                        if (response.IsSuccessStatusCode)
                            try
                            {
                                string JsonContent = await response.Content.ReadAsStringAsync();
                                List<News> news = DeserializeNewsResponse(JsonContent);
                                using (AvaTradeAppDBContext _context = _serviceScopeFactory
                                                                    .CreateScope()
                                                                    .ServiceProvider
                                                                    .GetRequiredService<AvaTradeAppDBContext>())
                                {

                                    var newNews = news.Where(item => !_context.News.Any(n => n.ExternalId == item.ExternalId)).ToList();

                                    if (news.Any())
                                    {
                                        try
                                        {
                                            var subscriberList = _context.Users.Where(u => u.IsSubscribed == true).ToList();
                                            if (subscriberList.Any())
                                            {
                                                StringBuilder emailTemplate = new StringBuilder();
                                                emailTemplate.Append(MailInfo.TemplateFirst);

                                                foreach (var newsItem in news)
                                                {
                                                    string itemBody = "<div class=\"news-item\">";
                                                    itemBody += $"<div class=\"title\">Title: {newsItem.Title}</div>";
                                                    itemBody += $"<div class=\"author\">Author: {newsItem.Author}</div>";
                                                    itemBody += $"<div class=\"description\">Description: {newsItem.Description}</div>";
                                                    itemBody += $"<div class=\"published\">Published: {newsItem.Published}</div>";
                                                    itemBody += "<a href=\"https://polygon.io/\" class=\"article-link\" target=\"_blank\">Read Full Article </a>";
                                                    itemBody += "</div>";
                                                    emailTemplate.Append(itemBody);
                                                }
                                                emailTemplate.Append(MailInfo.TemplateLast);
                                                foreach (var subscriber in subscriberList)
                                                {
                                                    await EmailHelper.SendEmailAsync(fromEmail, fromPassword, subscriber.Email, MailInfo.Subject, emailTemplate.ToString());
                                                }
                                            }
                                            await _context.News.AddRangeAsync(newNews);
                                            await _context.SaveChangesAsync();
                                        }
                                        catch (Exception ex)
                                        {
                                            _logger.LogInformation("BackgroundService error: {error}", ex.Message);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                _logger.LogInformation("BackgroundService error: {error}", ex.Message);
                            }
                    }
                    await Task.Delay((int)TimeSpan.FromMinutes(60).TotalMilliseconds, stoppingToken);
                }

            }
        }

        public static List<News> DeserializeNewsResponse(string jsonResponse)
        {
            var jsonDocument = JsonDocument.Parse(jsonResponse);
            var results = jsonDocument.RootElement.GetProperty("results");

            var newsList = new List<News>();
            foreach (var result in results.EnumerateArray())
            {
                var publisherElement = result.GetProperty("publisher");

                var publisher = new Publisher
                {
                    Name = publisherElement.GetProperty("name").GetString()
                };

                var news = new News
                {
                    Title = result.GetProperty("title").GetString(),
                    Author = result.GetProperty("author").GetString(),
                    ImageUrl = result.GetProperty("image_url").GetString(),
                    ExternalId = result.GetProperty("id").GetString(),
                    ArticleUrl = result.GetProperty("article_url").GetString(),
                    Description = result.GetProperty("description").GetString(),
                    Publisher = publisher,
                    Published = DateTime.Parse(result.GetProperty("published_utc").GetString()),
                    Keywords = result.GetProperty("keywords").EnumerateArray().Select(k => k.GetString()).ToList()
                };

                newsList.Add(news);
            }
            return newsList;
        }
    }
}
