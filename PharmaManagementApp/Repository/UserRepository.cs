using Microsoft.EntityFrameworkCore;
using PharmaManagementApp.Data;
using PharmaManagementApp.Models;

namespace PharmaManagementApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IDynamicDbContextFactory _dbContextFactory;
        private readonly string _connectionString;

        public UserRepository(IDynamicDbContextFactory dbContextFactory, string connectionString)
        {
            _dbContextFactory = dbContextFactory;
            _connectionString = connectionString;
        }

        private DynamicDbContext CreateContext()
        {
            return _dbContextFactory.CreateDbContext(_connectionString);
        }
        public async Task<IEnumerable<UserTable>> GetAllAsync()
        {
            using (var context = CreateContext())
            {
                return await context.UserTable.ToListAsync();
            }
        }

        public async Task<UserTable> GetByIdAsync(int UserId)
        {
            using (var context = CreateContext())
            {
                return await context.UserTable.FirstOrDefaultAsync(u => u.UserId == UserId);
            }
        }

        public async Task AddAsync(UserTable model)
        {
            using (var context = CreateContext())
            {
                await context.UserTable.AddAsync(model);
                await context.SaveChangesAsync();
            }
        }

        public void Update(UserTable model)
        {
            using (var context = CreateContext())
            {
                context.UserTable.Update(model);
                context.SaveChanges();
            }
        }

        public void Delete(UserTable model)
        {
            using (var context = CreateContext())
            {
                context.UserTable.Remove(model);
                context.SaveChanges();
            }
        }
    }
}
