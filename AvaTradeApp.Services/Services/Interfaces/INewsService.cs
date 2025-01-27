using AvaTradeApp.Domain.Entities;

namespace AvaTradeApp.Infrastructure.Services.Interfaces
{
    /// <summary>
    /// The INewsService interface defines methods for retrieving news data, including fetching all news, filtering news by keyword, limiting results, and getting the latest news, with optional filtering based on a time period.
    /// </summary>
    public interface INewsService
    {
        Task<IEnumerable<News>> GetLatestNews();
        Task<IEnumerable<News>> GetAllNewsAsync();
        Task<IEnumerable<News>> GetAllNewsWithGivingDay(int days);
        Task<IEnumerable<News>> GetAllNewsPerInstrument(string keyword);
        Task<IEnumerable<News>> GetAllNewsPerInstrumentWithLimit(string keyword, int limit);
    }
}
