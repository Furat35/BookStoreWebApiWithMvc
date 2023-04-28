using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserStore.WebMVC.ApiServices.Abstract;
using WebApi.Core.ActionAttributes;
using WebApi.Core.Consts;
using WebApi.Core.Models.AppUser;
using WebApi.Core.RequestFilters.User;

namespace BookStore.WebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class UserController : Controller
    {
        #region Fields
        private readonly IUserService _UserService;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public UserController(IUserService UserService, IMapper mapper)
        {
            _UserService = UserService;
            _mapper = mapper;
        }
        #endregion

        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.AdminRole}, {RoleConsts.EditorRole}")]
        public async Task<IActionResult> Index([FromQuery] UserRequestFilter filter = null)
        {
            var result = await _UserService
                .GetAppUsersAsync(filter);
            if (result.response.IsSuccess)
            {
                ViewBag.metadata = result.metadata;
                return View(result.response.Content);
            }

            return NotFound(result.response.ErrorDetails);
        }

        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.AdminRole}, {RoleConsts.EditorRole}")]
        public async Task<IActionResult> GetUsers([FromQuery] UserRequestFilter filter = null)
        {
            var result = await _UserService
                .GetAppUsersAsync(filter);
            if (result.response.IsSuccess)
            {
                ViewBag.metadata = result.metadata;
                return PartialView("_GetUsers", result.response.Content);
            }

            return NotFound(result.response.ErrorDetails);
        }

        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.AdminRole}, {RoleConsts.EditorRole}")]
        public async Task<IActionResult> GetUser([FromRoute] Guid id)
        {
            var response = await _UserService.GetAppUserAsync(id);
            if (response.IsSuccess)
                return View(response.Content);

            return NotFound(response.ErrorDetails);
        }

        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.AdminRole}")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateModelContent]
        [Authorize(Roles = $"{RoleConsts.AdminRole}")]
        public async Task<IActionResult> Add([FromBody] AppUserAddDto UserAddDto)
        {
            var response = await _UserService.AddAppUserAsync(UserAddDto);
            if (response.IsSuccess)
                return RedirectToAction(nameof(Index));

            return NotFound();
        }

        [Authorize(Roles = $"{RoleConsts.AdminRole}")]
        public async Task<IActionResult> Update([FromRoute] Guid id)
        {
            var response = await _UserService.GetAppUserAsync(id);
            if (response.IsSuccess)
            {
                var map = _mapper.Map<AppUserUpdateDto>(response.Content);
                return View(map);
            }

            return NotFound(response.ErrorDetails);
        }

        [HttpPost]
        [ValidateModelContent]
        [Authorize(Roles = $"{RoleConsts.AdminRole}")]
        public async Task<IActionResult> Update([FromForm] AppUserUpdateDto UserUpdateDto)
        {
            var response = await _UserService.UpdateAppUserAsync(UserUpdateDto);
            if (response.IsSuccess)
                return View();

            return NotFound();
        }


        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.AdminRole}")]
        public async Task<IActionResult> SafeDelete([FromRoute] Guid id)
        {
            var response = await _UserService.SafeDeleteAppUserAsync(id);
            if (response.IsSuccess)
                return RedirectToAction(nameof(Index));

            return NotFound();
        }
    }
}
