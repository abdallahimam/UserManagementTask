using Microsoft.AspNetCore.Mvc;
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
                    Expires = DateTime.Now.AddMinutes(30),  // Cookie expiration time
                    HttpOnly = true,  // Make cookie inaccessible to JavaScript
                    IsEssential = true,  // Mark as essential for GDPR
                    SameSite = SameSiteMode.Strict  // Prevent CSRF attacks by restricting cookie access
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
            return View(new LoginModel());
        }
    }
}
