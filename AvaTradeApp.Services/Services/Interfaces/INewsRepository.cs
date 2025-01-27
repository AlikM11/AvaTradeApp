using AvaTradeApp.Domain.Entities;

namespace AvaTradeApp.Infrastructure.Services.Interfaces
{
    /// <summary>
    /// The INewsRepository interface extends IRepository<News> and provides additional methods for retrieving news data, such as fetching all news, filtering by keyword, applying time-based filters, limiting results, and retrieving the latest news.
    /// </summary>
    public interface INewsRepository : IRepository<News>
    {
        Task<IEnumerable<News>> GetLatestNews();
        Task<IEnumerable<News>> GetAllNewsAsync();
        Task<IEnumerable<News>> GetAllNewsWithGivingDay(int days);
        Task<IEnumerable<News>> GetAllNewsPerInstrument(string keyword);
        Task<IEnumerable<News>> GetAllNewsPerInstrumentWithLimit(string keyword, int limit);
    }
}
