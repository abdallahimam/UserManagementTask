using Azure.Core;
using UserManagementTask.Application.DTOs;
using UserManagementTask.Core.Entities;
using UserManagementTask.Core.Interfaces;
using UserManagementTask.WebUI.Models;

namespace UserManagementTask.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork  _unitOfWork;
        private readonly HttpContext _context;

        public bool IsAuthenticated => IsLoggedin();

        public AuthService(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _context = contextAccessor.HttpContext;
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

        private bool IsLoggedin()
        {
            return _context.Request.Cookies.TryGetValue("UserModel", out var cookie);
        }

        public string? GetLoggedinUser()
        {
            var result = _context.Request.Cookies.TryGetValue("UserModel", out var cookie);
            if (!result)
                return null;

            var deserializedModel = System.Text.Json.JsonSerializer.Deserialize<UserLoginDto>(cookie);

            if (deserializedModel is null)
                return null;

            return deserializedModel.Username;
        }

        public bool Logout()
        {
            try
            {
                _context.Response.Cookies.Delete("UserModel");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
