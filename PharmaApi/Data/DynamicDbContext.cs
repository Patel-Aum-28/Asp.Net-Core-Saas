using Microsoft.EntityFrameworkCore;
using PharmaApi.Models;

namespace PharmaApi.Data
{
    public class DynamicDbContext : AppDbContext
    {
        public string ConnectionString { get; set; }

        public DynamicDbContext(DbContextOptions<DynamicDbContext> options) : base(options)
        {
        }

        public DynamicDbContext(string connectionString) : base(GetOptions(connectionString)) { }

        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder<DynamicDbContext>(), connectionString).Options;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrEmpty(ConnectionString)) optionsBuilder.UseSqlServer(ConnectionString);
        }

        public DbSet<UserTable> UserTable { get; set; }
    }
}
