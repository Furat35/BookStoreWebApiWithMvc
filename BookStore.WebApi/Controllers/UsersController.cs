using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebApi.Core.ActionAttributes;
using WebApi.Core.Consts;
using WebApi.Core.Models.AppUser;
using WebApi.Core.RequestFilters.User;
using WebApi.Service.Abstract;
using WebApi.Service.ActionAttributes;

namespace BookStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        #region Fields
        private readonly IAppUserService _userService;
        #endregion

        #region Ctor
        public UsersController(IAppUserService userService)
        {
            _userService = userService;
        }
        #endregion

        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.AdminRole}, {RoleConsts.EditorRole}")]
        [ResponseCache(Duration = 300)]
        [CacheData(Duration = 5)]
        public async Task<IActionResult> GetUsers([FromQuery] UserRequestFilter filters = null)
        {
            var result = await _userService
                .GetUsersAsync(_ => !_.IsDeleted, filters);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.metadata));

            return Ok(result.users);
        }

        [HttpGet("{id:guid}")]
        [Authorize(Roles = $"{RoleConsts.AdminRole}, {RoleConsts.EditorRole}")]
        public async Task<IActionResult> GetUsers(Guid id)
        {
            var user = await _userService
                .GetUserByGuidAsync(id);
            return Ok(user);
        }

        [HttpPost("[action]")]
        [ValidateModelContent]
        [RemoveCache]
        [Authorize(Roles = $"{RoleConsts.AdminRole}")]
        public async Task<IActionResult> Add([FromBody] AppUserAddDto entity)
        {
            var user = await _userService
                .AddUserAsync(entity);
            return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
        }

        [HttpPost("[action]")]
        [ValidateModelContent]
        [RemoveCache]
        [Authorize(Roles = $"{RoleConsts.AdminRole}")]
        public async Task<IActionResult> Update([FromBody] AppUserUpdateDto entity)
        {
            await _userService
                .UpdateUserAsync(entity);
            return NoContent();
        }

        [HttpPost("[action]/{id:guid}")]
        [RemoveCache]
        [Authorize(Roles = $"{RoleConsts.AdminRole}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _userService
                .SafeDeleteUserAsync(id);
            return NoContent();
        }
    }
}
