using Microsoft.EntityFrameworkCore;
using PharmaApi.Models;

namespace PharmaApi.Data
{
    public class AppDbContext : DbContext
    {
        protected AppDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

        public override int SaveChanges()
        {
            var timestamp = DateTime.Now;

            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = timestamp;
                    entry.Entity.UpdatedAt = timestamp;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = timestamp;
                }
            }

            return base.SaveChanges();
        }
    }
}
