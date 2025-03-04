using PharmaManagementApp.Models;
namespace PharmaManagementApp.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserTable>> GetAllAsync();
        Task<UserTable> GetByIdAsync(int UserId);
        Task AddAsync(UserTable model);
        void Update(UserTable model);
        void Delete(UserTable model);
    }
}
