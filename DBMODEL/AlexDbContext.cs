using COMMON.Models;
using DBMODEL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DBMODEL
{
    public class AlexDbContext : DbContext
    {
        public AlexDbContext(string connectionString) : base(GetOptions(connectionString))
        {
            Database.Migrate();
        }

        public AlexDbContext(DbContextOptions<AlexDbContext> options) : base(GetOptions(AppSettings.ConnectionString))
        {
            Database.Migrate();
        }

        public DbSet<Exchange> Exchanges { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockResult> StockResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


        private static DbContextOptions GetOptions(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("Connection string cannot be null");
            }
            
            return NpgsqlDbContextOptionsBuilderExtensions.UseNpgsql(new DbContextOptionsBuilder(), connectionString).Options;
        }
    }
}
