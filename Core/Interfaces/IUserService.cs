using UserManagementTask.Core.Entities;

namespace UserManagementTask.Core.Interfaces
{
    public interface IUserService
    {
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int Id);
    }
}
