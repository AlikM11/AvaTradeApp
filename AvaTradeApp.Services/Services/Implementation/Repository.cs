using AvaTradeApp.Services;
using System.Linq.Expressions;
using AvaTradeApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using AvaTradeApp.Infrastructure.Services.Interfaces;

namespace AvaTradeApp.Infrastructure.Services.Implementation
{
    /// <summary>
    /// The Repository<T> class provides generic methods for basic CRUD operations on entities, such as adding, querying, and saving changes in the database
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DbSet<T> dbSet;
        protected readonly AvaTradeAppDBContext _context;
        public Repository(AvaTradeAppDBContext context)
        {
            _context = context;
            dbSet = context.Set<T>();
        }


        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> where)
        {
            T? data = await dbSet.FirstOrDefaultAsync(where);
            return data;
        }
        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await dbSet.AddRangeAsync(entities);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
