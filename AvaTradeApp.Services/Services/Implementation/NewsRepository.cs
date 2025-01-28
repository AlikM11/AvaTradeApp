using AvaTradeApp.Services;
using AvaTradeApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using AvaTradeApp.Infrastructure.Services.Interfaces;

namespace AvaTradeApp.Infrastructure.Services.Implementation
{
    public class NewsRepository : Repository<News>, INewsRepository
    {
        public NewsRepository(AvaTradeAppDBContext context) : base(context){}

        public async Task<IEnumerable<News>> GetAllNewsAsync()
        {
            List<News> news =  await _context.News
                                    .Include(n => n.Publisher)
                                    .AsNoTracking()
                                    .ToListAsync();
            return news;
        }

        public async Task<IEnumerable<News>> GetAllNewsWithGivingDayAsync(int days)
        {
            List<News> news = await _context.News
                                    .Where(n => n.Published >= DateTime.Today.AddDays(-days))
                                    .Include(n => n.Publisher)
                                    .AsNoTracking()
                                    .ToListAsync();
            return news;
        }

        public async Task<IEnumerable<News>> GetAllNewsPerInstrumentWithLimitAsync(string keyword, int limit)
        {
            var lowerKeyword = keyword.ToLower();

            var news = await _context.News
                .Include(n => n.Publisher)
                .AsNoTracking()
                .ToListAsync();

            return news
                .Where(n => n.Keywords.Any(k => k.ToLower() == lowerKeyword))
                .Take(limit);
        }

        public async Task<IEnumerable<News>> GetAllNewsPerInstrumentAsync(string keyword)
        {
            var lowerKeyword = keyword.ToLower();

            var news = await _context.News
                .Include(n => n.Publisher)
                .AsNoTracking()
                .ToListAsync();

            return news
                .Where(n => n.Keywords.Any(k => k.ToLower() == lowerKeyword));
        }

        public async Task<IEnumerable<News>> GetLatestNewsAsync()
        {
            var news = await _context.News
                .AsNoTracking()
                .OrderByDescending(n => n.Published)
                .GroupBy(n => n.ExternalId)
                .Select(g => g.First())
                .Distinct()
                .Take(5)
                .ToListAsync();

            return news;
        }
    }
}
