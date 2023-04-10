using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Core.ActionAttributes;
using WebApi.Core.Consts;
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
        [Authorize(Roles = $"{RoleConsts.AdminRole}, {RoleConsts.EditorRole}")]
        public async Task<IActionResult> Users()
        {
            var users = await _userService
                .GetUsersAsync(_ => !_.IsDeleted);
            return Ok(users);
        }

        [HttpGet("{id:guid}")]
        [Authorize(Roles = $"{RoleConsts.AdminRole}, {RoleConsts.EditorRole}")]
        public async Task<IActionResult> Users(Guid id)
        {
            var user = await _userService
                .GetUserByGuidAsync(id);
            return Ok(user);
        }

        [HttpPost("[action]")]
        [ValidateModelContent]
        [Authorize(Roles = $"{RoleConsts.AdminRole}")]
        public async Task<IActionResult> Add([FromBody] AppUserAddDto entity)
        {
            var user = await _userService
                .AddUserAsync(entity);
            return CreatedAtAction(nameof(Users), new { id = user.Id }, user);
        }

        [HttpPost("[action]")]
        [ValidateModelContent]
        [Authorize(Roles = $"{RoleConsts.AdminRole}")]
        public async Task<IActionResult> Update([FromBody] AppUserUpdateDto entity)
        {
            await _userService
                .UpdateUserAsync(entity);
            return NoContent();
        }

        [HttpPost("[action]/{id:guid}")]
        [Authorize(Roles = $"{RoleConsts.AdminRole}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _userService
                .DeleteUserAsync(id);
            return NoContent();
        }
    }
}
