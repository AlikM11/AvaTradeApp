using Moq;
using AvaTradeApp.Domain.Entities;
using AvaTradeApp.Infrastructure.Services.Interfaces;
using AvaTradeApp.Infrastructure.Services.Implementation;

namespace NewsServiceTests
{
    public class NewsServiceTests
    {
        private readonly Mock<INewsRepository> _mockNewsRepository;
        private readonly INewsService _newsService;

        public NewsServiceTests()
        {
            _mockNewsRepository = new Mock<INewsRepository>();
            _newsService = new NewsService(_mockNewsRepository.Object);
        }

        [Fact]
        public async Task GetAllNewsAsync_ShouldReturnNewsList_WhenNewsExist()
        {
            // Arrange
            var newsList = new List<News>
            {
                new News { Title = "Test News 1", Description = "Content 1" },
                new News { Title = "Test News 2", Description = "Content 2" }
            };
            _mockNewsRepository.Setup(repo => repo.GetAllNewsAsync()).ReturnsAsync(newsList);

            var result = await _newsService.GetAllNewsAsync();
            
            Assert.Equal(2, result.Count());
            Assert.Equal("Test News 1", result.First().Title);
        }

        [Fact]
        public async Task GetAllNewsWithGivingDay_ShouldReturnFilteredNews_WhenDaysAreGiven()
        {
            var newsList = new List<News>
            {
                new News { Title = "Recent News", Published = DateTime.Now.AddDays(-3) }
            };
            var days = 5;
            _mockNewsRepository.Setup(repo => repo.GetAllNewsWithGivingDay(days)).ReturnsAsync(newsList);

            var result = await _newsService.GetAllNewsWithGivingDay(days);

            Assert.Single(result);
            Assert.Equal("Recent News", result.First().Title);
        }

        [Fact]
        public async Task GetAllNewsPerInstrumentWithLimit_ShouldReturnLimitedNews_WhenKeywordAndLimitAreGiven()
        {
            var newsList = new List<News>
            {
                new News { Title = "Instrument News 1" },
                new News { Title = "Instrument News 2" }
            };
            var keyword = "Instrument";
            var limit = 2;
            _mockNewsRepository.Setup(repo => repo.GetAllNewsPerInstrumentWithLimit(keyword, limit)).ReturnsAsync(newsList);

            var result = await _newsService.GetAllNewsPerInstrumentWithLimit(keyword, limit);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetLatestNews_ShouldReturnLatestNews_WhenCalled()
        {
            var latestNews = new List<News>
            {
                new News { Title = "Latest News 1" },
                new News { Title = "Latest News 2" }
            };
            _mockNewsRepository.Setup(repo => repo.GetLatestNews()).ReturnsAsync(latestNews);
            
            var result = await _newsService.GetLatestNews();

            Assert.Equal(2, result.Count());
            Assert.Equal("Latest News 1", result.First().Title);
        }
    }
}