using AvaTradeApp.Domain.Entities;
using AvaTradeApp.Infrastructure.Services.Interfaces;

namespace AvaTradeApp.Infrastructure.Services.Implementation
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepo;

        public NewsService(INewsRepository newsRepo)
        {
            _newsRepo = newsRepo;
        }

        public async Task<IEnumerable<News>> GetAllNewsAsync()
        {
            return await _newsRepo.GetAllNewsAsync();

        }

        public async Task<IEnumerable<News>> GetAllNewsWithGivingDayAsync(int days)
        {
            return await _newsRepo.GetAllNewsWithGivingDayAsync(days);
        }

        public async Task<IEnumerable<News>> GetAllNewsPerInstrumentWithLimitAsync(string keyword, int limit = 10)
        {
            return await _newsRepo.GetAllNewsPerInstrumentWithLimitAsync(keyword, limit);
        }

        public async Task<IEnumerable<News>> GetAllNewsPerInstrumentAsync(string keyword)
        {
            return await _newsRepo.GetAllNewsPerInstrumentAsync(keyword);
        }
        public async Task<IEnumerable<News>> GetLatestNewsAsync()
        {
            return await _newsRepo.GetLatestNewsAsync();
        }
    }
}
