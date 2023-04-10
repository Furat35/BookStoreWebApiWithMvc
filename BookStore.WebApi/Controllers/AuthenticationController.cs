using Microsoft.AspNetCore.Mvc;
using WebApi.Core.Models.Authentication;
using WebApi.Service.Abstract;

namespace BookStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authService;

        public AuthenticationController(
           IAuthenticationService authService)
        {
            _authService = authService;
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginDto entity)
        {
            var result = await _authService.LoginAsync(entity);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] RegisterDto entity)
        {
            await _authService.RegisterAsync(entity);
            return CreatedAtAction(nameof(Login), entity);
        }
    }
}
