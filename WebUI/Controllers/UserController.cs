using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagementTask.Application.DTOs;
using UserManagementTask.Core.Entities;
using UserManagementTask.Core.Interfaces;
using UserManagementTask.WebUI.Models;

namespace UserManagementTask.WebUI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _userService.GetAllUsersAsync();
            var users = result.Select(u => new UserDto
            {
                Id = u.Id,
                Username = u.Username,
                UserFullName = u.UserFullName,
                IsActive = u.IsActive,
                DateOfBirth = u.DateOfBirth,
                CreationDate = u.CreationDate
            });

            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateUserModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            var user = new User
            {
                Username = model.Username,
                UserFullName = model.UserFullName,
                Password = model.Password,
                IsActive = model.IsActive,
                DateOfBirth = model.DateOfBirth,
                CreationDate = DateTime.Now
            };

            await _userService.CreateUserAsync(user);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var user = await _userService.GetUserByIdAsync(Id);
            if (user is null)
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction(nameof(Index));
            }

            var model = new EditUserModel
            {
                Id = user.Id,
                Username = user.Username,
                UserFullName = user.UserFullName,
                IsActive = user.IsActive,
                DateOfBirth = user.DateOfBirth
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, EditUserModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (Id != model.Id)
            {
                TempData["ErrorMessage"] = "User ID mismatch.";
                return RedirectToAction(nameof(Index));
            }

            var user = await _userService.GetUserByIdAsync(Id);

            user.Username = model.Username;
            user.UserFullName = model.UserFullName;
            user.Password = model.Password;
            user.IsActive = model.IsActive;
            user.DateOfBirth = model.DateOfBirth;

            await _userService.UpdateUserAsync(user);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id)
        {
            var user = await _userService.GetUserByIdAsync(Id);
            if (user is null)
            {
                return NotFound(new { message = "User not found." });
            }

            await _userService.DeleteUserAsync(Id);

            TempData["SuccessMessage"] = "User deleted successfully.";

            return RedirectToAction(nameof(Index));

            //return Ok(new { message = "User deleted successfully." });
        }
    }
}
