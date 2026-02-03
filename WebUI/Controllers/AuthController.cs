using Microsoft.AspNetCore.Mvc;
using UserManagementTask.Application.DTOs;
using UserManagementTask.Core.Interfaces;
using UserManagementTask.WebUI.Models;

namespace UserManagementTask.WebUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _authService.LoginAsync(model.Username!, model.Password!);

            if (result.Success)
            {

                var serializedModel = System.Text.Json.JsonSerializer.Serialize(result.Data);

                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(30),
                    HttpOnly = true,
                    IsEssential = true,
                    SameSite = SameSiteMode.Strict
                };

                Response.Cookies.Append("UserModel", serializedModel, cookieOptions);
                
                TempData["WelcomeMessage"] = $"Welcome, {model.Username}!";

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, result.Message!);

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            var user = _authService.GetLoggedinUser();

            if (string.IsNullOrEmpty(user))
                return View(new LoginModel());

            TempData["WelcomeMessage"] = $"Welcome, {user}!";

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            var logoutSuccess = _authService.Logout();
            if (logoutSuccess)
            {
                return RedirectToAction("Login", "Auth");
            }
            TempData["ErrorMessage"] = "Logout failed. Please try again.";
            return RedirectToAction("Index", "Home");
        }
    }
}
