using AvaTradeApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AvaTradeApp.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class AvaTradeAppDBContext : IdentityDbContext<User>
    {
        public DbSet<News> News { get; set; }

        public AvaTradeAppDBContext(DbContextOptions<AvaTradeAppDBContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<News>()
                .HasOne(n => n.Publisher)
                .WithMany()
                .HasForeignKey("PublisherId");

            builder.Entity<News>()
                .Property(n => n.Keywords)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
        }
    }
}
