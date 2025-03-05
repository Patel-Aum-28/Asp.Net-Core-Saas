using PharmaApi.Models;
namespace PharmaApi.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserTable>> GetAllAsync();
        Task<UserTable> GetByIdAsync(int UserId);
        Task<UserTable> AddAsync(UserTable model);
        UserTable Update(UserTable model);
        UserTable Delete(UserTable model);
    }
}
