using System.Linq.Expressions;
using AvaTradeApp.Domain.Entities;

namespace AvaTradeApp.Infrastructure.Services.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task AddAsync(T entity);
        Task SaveChangesAsync();
        Task AddRangeAsync(IEnumerable<T> entities);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> where);
    }
}
