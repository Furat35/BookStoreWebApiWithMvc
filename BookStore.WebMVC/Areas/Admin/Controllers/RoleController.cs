using AutoMapper;
using BookStore.WebMVC.ApiServices.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Core.ActionAttributes;
using WebApi.Core.Consts;
using WebApi.Core.Models.AppRole;
using WebApi.Core.RequestFilters.Role;

namespace BookStore.WebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{RoleConsts.AdminRole}")]
    public class RoleController : Controller
    {
        #region Fields
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public RoleController(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] RoleRequestFilter filter = null)
        {
            var result = await _roleService
                .GetAppRolesAsync(filter);
            if (result.response.IsSuccess)
            {
                ViewBag.metadata = result.metadata;
                return View(result.response.Content);
            }

            return NotFound(result.response.ErrorDetails);
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles([FromQuery] RoleRequestFilter filter = null)
        {
            var result = await _roleService
                .GetAppRolesAsync(filter);
            if (result.response.IsSuccess)
            {
                ViewBag.metadata = result.metadata;
                return PartialView("_GetRoles", result.response.Content);
            }

            return NotFound(result.response.ErrorDetails);
        }

        [HttpGet]
        public async Task<IActionResult> GetRole([FromRoute] Guid id)
        {
            var response = await _roleService.GetAppRoleAsync(id);
            if (response.IsSuccess)
                return View(response.Content);

            return NotFound(response.ErrorDetails);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateModelContent]
        public async Task<IActionResult> Add([FromBody] AppRoleAddDto roleAddDto)
        {
            var response = await _roleService.AddAppRoleAsync(roleAddDto);
            if (response.IsSuccess)
                return RedirectToAction(nameof(Index));

            return NotFound();
        }

        public async Task<IActionResult> Update([FromRoute] Guid id)
        {
            var response = await _roleService.GetAppRoleAsync(id);
            if (response.IsSuccess)
            {
                var map = _mapper.Map<AppRoleUpdateDto>(response.Content);
                return View(map);
            }

            return NotFound(response.ErrorDetails);
        }

        [HttpPost]
        [ValidateModelContent]
        public async Task<IActionResult> Update([FromForm] AppRoleUpdateDto roleUpdateDto)
        {
            var response = await _roleService.UpdateAppRoleAsync(roleUpdateDto);
            if (response.IsSuccess)
                return View();

            return NotFound();
        }


        [HttpGet]
        public async Task<IActionResult> SafeDelete([FromRoute] Guid id)
        {
            var response = await _roleService.SafeDeleteAppRoleAsync(id);
            if (response.IsSuccess)
                return RedirectToAction(nameof(Index));

            return NotFound();
        }
    }
}
