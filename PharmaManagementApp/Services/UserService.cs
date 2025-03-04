using PharmaManagementApp.Models;
using PharmaManagementApp.Repository;

namespace PharmaManagementApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UserTable>> GetAllUsersAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<UserTable> GetUserByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task AddUserAsync(UserTable model)
        {
            await _repository.AddAsync(model);
        }
        public async void UpdateUser(UserTable model)
        {
            _repository.Update(model);
        }
        public async void DeleteUser(UserTable model)
        {
            _repository.Delete(model);
        }
    }
}
