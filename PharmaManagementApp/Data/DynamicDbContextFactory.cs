using Microsoft.EntityFrameworkCore;

namespace PharmaManagementApp.Data
{
    public class DynamicDbContextFactory : IDynamicDbContextFactory
    {
        public DynamicDbContext CreateDbContext(string connectionString)
        {
            var optionBuilder = new DbContextOptionsBuilder<DynamicDbContext>();
            optionBuilder.UseSqlServer(connectionString);
            return new DynamicDbContext(optionBuilder.Options, connectionString);
        }
    }
}
