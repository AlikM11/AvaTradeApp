using AvaTradeApp.Domain.Entities;

namespace AvaTradeApp.Infrastructure.Services.Interfaces
{
    /// <summary>
    /// The INewsService interface defines methods for retrieving news data, including fetching all news, filtering news by keyword, limiting results, and getting the latest news, with optional filtering based on a time period.
    /// </summary>
    public interface INewsService
    {
        Task<IEnumerable<News>> GetLatestNewsAsync();
        Task<IEnumerable<News>> GetAllNewsAsync();
        Task<IEnumerable<News>> GetAllNewsWithGivingDayAsync(int days);
        Task<IEnumerable<News>> GetAllNewsPerInstrumentAsync(string keyword);
        Task<IEnumerable<News>> GetAllNewsPerInstrumentWithLimitAsync(string keyword, int limit);
    }
}
