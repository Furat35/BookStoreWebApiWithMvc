using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Core.ActionAttributes;
using WebApi.Core.Models.Authentication;
using IAuthService = BookStore.WebMVC.ApiServices.Abstract.IAuthService;

namespace BookStore.WebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthenticationController : Controller
    {
        #region Fields
        private readonly IAuthService _authService;
        #endregion

        #region Ctor
        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }
        #endregion

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateModelContent]
        public async Task<IActionResult> Login([FromForm] LoginDto loginDto)
        {
            var response = await _authService.LoginAsync(loginDto, HttpContext);
            return response.IsSuccess
                ? RedirectToAction("Index", "Book", new {Area = "Admin"})
                : RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index","Book", new { Area = "Admin" });
            return View();
        }

        [HttpPost]
        [ValidateModelContent]
        public async Task<IActionResult> Register([FromForm] RegisterDto registerDto)
        {
            if (User.Identity.IsAuthenticated)
                return NotFound();
            var response = await _authService.RegisterAsync(registerDto);
            return response.IsSuccess
                ? RedirectToAction("Index","Book", new { Area = "Admin" })
                : CreatedAtAction(nameof(Login), response);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
