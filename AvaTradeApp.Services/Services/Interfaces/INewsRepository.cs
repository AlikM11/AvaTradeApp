using AvaTradeApp.Domain.Entities;

namespace AvaTradeApp.Infrastructure.Services.Interfaces
{
    /// <summary>
    /// The INewsRepository interface extends IRepository<News> and provides additional methods for retrieving news data, such as fetching all news, filtering by keyword, applying time-based filters, limiting results, and retrieving the latest news.
    /// </summary>
    public interface INewsRepository : IRepository<News>
    {
        Task<IEnumerable<News>> GetLatestNewsAsync();
        Task<IEnumerable<News>> GetAllNewsAsync();
        Task<IEnumerable<News>> GetAllNewsWithGivingDayAsync(int days);
        Task<IEnumerable<News>> GetAllNewsPerInstrumentAsync(string keyword);
        Task<IEnumerable<News>> GetAllNewsPerInstrumentWithLimitAsync(string keyword, int limit);
    }
}
