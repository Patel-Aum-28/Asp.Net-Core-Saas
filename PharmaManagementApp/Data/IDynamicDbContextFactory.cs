namespace PharmaManagementApp.Data
{
    public interface IDynamicDbContextFactory
    {
        DynamicDbContext CreateDbContext(string connectionString);
    }
}
