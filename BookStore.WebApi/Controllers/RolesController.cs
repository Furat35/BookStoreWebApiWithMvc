using Microsoft.AspNetCore.Mvc;
using WebApi.Core.ActionAttributes;
using WebApi.Core.Models.AppRole;
using WebApi.Service.Abstract;

namespace BookStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IAppRoleService _roleService;

        public RolesController(IAppRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> Roles()
        {
            var roles = await _roleService
                .GetRolesAsync(_ => !_.IsDeleted);
            return Ok(roles);
        }

        [HttpGet("[action]/{id:guid}")]
        public async Task<IActionResult> GetRole(Guid id)
        {
            var role = await _roleService
                .GetRoleByGuid(id);
            return Ok(role);
        }

        [HttpPost("[action]")]
        [ValidateModelContent]
        public async Task<IActionResult> Add([FromBody] AppRoleAddDto entity)
        {
            var role = await _roleService
                .AddRoleAsync(entity);
            return CreatedAtAction(nameof(GetRole), new { id = role.Id }, role);
        }

        [HttpPost("[action]")]
        [ValidateModelContent]
        public async Task<IActionResult> Update([FromBody] AppRoleUpdateDto entity)
        {
            await _roleService
                .UpdateRoleAsync(entity);
            return Ok();
        }

        [HttpPost("[action]/{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _roleService
                .DeleteRoleAsync(id);
            return Ok();
        }
    }
}
