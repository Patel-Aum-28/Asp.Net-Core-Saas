using Microsoft.EntityFrameworkCore;
using PharmaManagementApp.Models;

namespace PharmaManagementApp.Data
{
    public class DynamicDbContext : AppDbContext
    {
        protected readonly string _connectionString;
        public DynamicDbContext(DbContextOptions<DynamicDbContext> options, string connectionString) : base(options)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if(!optionsBuilder.IsConfigured && !string.IsNullOrWhiteSpace(_connectionString))
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        public DbSet<UserTable> UserTable { get; set; }
    }
}
