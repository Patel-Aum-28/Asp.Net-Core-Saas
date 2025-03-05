using Microsoft.EntityFrameworkCore;
using PharmaApi.Data;
using PharmaApi.Models;

namespace PharmaApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DynamicDbContext _dbContext;

        public UserRepository(DynamicDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<UserTable>> GetAllAsync()
        {
            return await _dbContext.UserTable.ToListAsync();
        }

        public async Task<UserTable> GetByIdAsync(int UserId)
        {
            return await _dbContext.UserTable.FirstOrDefaultAsync(u => u.UserId == UserId);
        }

        public async Task<UserTable> AddAsync(UserTable model)
        {
            await _dbContext.UserTable.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return await _dbContext.UserTable.FirstOrDefaultAsync(u => u.UserId == model.UserId);
        }

        public UserTable Update(UserTable model)
        {
            _dbContext.UserTable.Update(model);
            _dbContext.SaveChanges();

            return _dbContext.UserTable.FirstOrDefault(u => u.UserId == model.UserId);
        }

        public UserTable Delete(UserTable model)
        {
            _dbContext.UserTable.Remove(model);
            _dbContext.SaveChanges();

            return _dbContext.UserTable.FirstOrDefault(u => u.UserId == model.UserId);
        }
    }
}
