using UserManagementTask.Application.DTOs;
using UserManagementTask.Core.Entities;
using UserManagementTask.WebUI.Models;

namespace UserManagementTask.Core.Interfaces
{
    public interface IAuthService
    {
        Task<Result<UserLoginDto>> LoginAsync(string username, string password);
    }
}
