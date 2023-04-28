using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebApi.Core.ActionAttributes;
using WebApi.Core.Consts;
using WebApi.Core.Models.AppRole;
using WebApi.Core.RequestFilters.Role;
using WebApi.Service.Abstract;
using WebApi.Service.ActionAttributes;

namespace BookStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = $"{RoleConsts.AdminRole}")]
    public class RolesController : ControllerBase
    {
        #region Fields
        private readonly IAppRoleService _roleService;
        #endregion

        #region Ctor
        public RolesController(IAppRoleService roleService)
        {
            _roleService = roleService;
        }
        #endregion

        [HttpGet]
        [ResponseCache(Duration = 300)]
        [CacheData(Duration = 5)]
        public async Task<IActionResult> GetRoles([FromQuery] RoleRequestFilter filters = null)
        {
            //todo: buralari haller
            var result = await _roleService
                .GetRolesAsync(_ => !_.IsDeleted, filters);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.metadata));

            return Ok(result.roles);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetRoles(Guid id)
        {
            var role = await _roleService
                .GetRoleByGuid(id);
            return Ok(role);
        }

        [HttpPost("[action]")]
        [RemoveCache]
        [ValidateModelContent]
        public async Task<IActionResult> Add([FromBody] AppRoleAddDto entity)
        {
            var role = await _roleService
                .AddRoleAsync(entity);
            return CreatedAtAction(nameof(GetRoles), new { id = role.Id }, role);
        }

        [HttpPost("[action]")]
        [RemoveCache]
        [ValidateModelContent]
        public async Task<IActionResult> Update([FromBody] AppRoleUpdateDto entity)
        {
            await _roleService
                .UpdateRoleAsync(entity);
            return Ok();
        }

        [HttpPost("[action]/{id:guid}")]
        [RemoveCache]
        public async Task<IActionResult> SafeDelete([FromRoute] Guid id)
        {
            await _roleService
                .SafeDeleteRoleAsync(id);
            return Ok();
        }
    }
}
