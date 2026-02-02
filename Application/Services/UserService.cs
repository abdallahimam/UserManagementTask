using UserManagementTask.Core.Entities;
using UserManagementTask.Core.Interfaces;

namespace UserManagementTask.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateUserAsync(User user)
        {
            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            await _unitOfWork.UserRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _unitOfWork.UserRepository.GetAllAsync();
        }

        public async Task<User> GetUserByIdAsync(int Id)
        {
            return await _unitOfWork.UserRepository.GetByIdAsync(Id);
        }

        public async Task UpdateUserAsync(User user)
        {
            await _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
