using PharmaManagementApp.Models;

namespace PharmaManagementApp.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserTable>> GetAllUsersAsync();
        Task<UserTable> GetUserByIdAsync(int id);
        Task AddUserAsync(UserTable model);
        void UpdateUser(UserTable model);
        void DeleteUser(UserTable model);
    }
}
