using Microsoft.AspNetCore.Mvc;
using WebApi.Core.ActionAttributes;
using WebApi.Core.Models.AppUser;
using WebApi.Service.Abstract;

namespace BookStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAppUserService _userService;

        public UsersController(IAppUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Users()
        {
            var users = await _userService
                .GetUsersAsync(_ => !_.IsDeleted);
            return Ok(users);
        }

        [HttpGet("[action]/{id:guid}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await _userService
                .GetUserByGuidAsync(id);
            return Ok(user);
        }

        [HttpPost("[action]")]
        [ValidateModelContent]
        public async Task<IActionResult> Add([FromBody] AppUserAddDto entity)
        {
            var user = await _userService
                .AddUserAsync(entity);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPost("[action]")]
        [ValidateModelContent]
        public async Task<IActionResult> Update([FromBody] AppUserUpdateDto entity)
        {
            await _userService
                .UpdateUserAsync(entity);
            return NoContent();
        }

        [HttpPost("[action]/{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _userService
                .DeleteUserAsync(id);
            return NoContent();
        }
    }
}
