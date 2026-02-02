using UserManagementTask.Application.DTOs;
using UserManagementTask.Core.Entities;
using UserManagementTask.Core.Interfaces;
using UserManagementTask.WebUI.Models;

namespace UserManagementTask.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork  _unitOfWork;

        public AuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<UserLoginDto>> LoginAsync(string username, string password)
        {
            var user = await _unitOfWork.UserRepository.GetByCriteriaAsync(u => u.Username == username);

            if (user is null)
            {
                return Result<UserLoginDto>.FailResponse("User not found", 404);
            }

            if (user != null && user.Password != password)
            {
                return Result<UserLoginDto>.FailResponse("Username or Password incorrect", 401);
            }

            if (user != null && user.Password == password && !user.IsActive)
            {
                return Result<UserLoginDto>.FailResponse("User is inactive", 403);
            }

            var userLoginDto = new UserLoginDto
            {
                Id = user!.Id,
                Username = user.Username,
                UserFullName = user.UserFullName
            };

            return Result<UserLoginDto>.SuccessResponse(userLoginDto, 200, "Login successful");
        }
    }
}
