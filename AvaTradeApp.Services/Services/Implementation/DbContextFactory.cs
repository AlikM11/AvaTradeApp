using AvaTradeApp.Domain.Statics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Design;

namespace AvaTradeApp.Services
{
    public class DbContextFactory : IDesignTimeDbContextFactory<AvaTradeAppDBContext>
    {
        public AvaTradeAppDBContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(ConfigurationInfo.AppSettings)
                .Build();

            var dbContextBuilder = new DbContextOptionsBuilder<AvaTradeAppDBContext>();

            string connectionString = configuration.GetConnectionString(ConfigurationInfo.ConnectionName);

            dbContextBuilder.UseSqlServer(connectionString);

            return new AvaTradeAppDBContext(dbContextBuilder.Options);
        }
    }
}
