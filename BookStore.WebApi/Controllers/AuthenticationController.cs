using Microsoft.AspNetCore.Mvc;
using WebApi.Core.ActionAttributes;
using WebApi.Core.Models.Authentication;
using WebApi.Service.Abstract;

namespace BookStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        #region Fields
        private readonly IAuthenticationService _authService;
        #endregion

        #region Ctor
        public AuthenticationController(
           IAuthenticationService authService)
        {
            _authService = authService;
        }
        #endregion

        [HttpPost("[action]")]
        [ValidateModelContent]
        public async Task<IActionResult> Login([FromBody] LoginDto entity)
        {
            var result = await _authService
                .LoginAsync(entity);
            return Ok(result);
        }

        [HttpPost("[action]")]
        [ValidateModelContent]
        public async Task<IActionResult> Register([FromBody] RegisterDto entity)
        {
            await _authService
                .RegisterAsync(entity);
            return CreatedAtAction(nameof(Login), entity);
        }
    }
}
