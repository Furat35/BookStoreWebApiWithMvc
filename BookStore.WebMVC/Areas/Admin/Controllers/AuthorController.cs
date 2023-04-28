using AuthorStore.WebMVC.ApiServices.Abstract;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Core.ActionAttributes;
using WebApi.Core.Consts;
using WebApi.Core.Models.Author;
using WebApi.Core.RequestFilters.Auhtor;

namespace BookStore.WebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AuthorController : Controller
    {
        #region Fields
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public AuthorController(IAuthorService authorService, IMapper mapper)
        {
            _authorService = authorService;
            _mapper = mapper;
        }
        #endregion

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index([FromQuery] AuthorRequestFilter filters)
        {
            var result = await _authorService
                .GetAuthorsAsync(filters);
            if (result.response.IsSuccess)
            {
                ViewBag.metadata = result.metadata;
                return View(result.response.Content);
            }

            return NotFound(result.response.ErrorDetails);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAuthor([FromRoute] Guid id)
        {
            var response = await _authorService.GetAuthorAsync(id);
            if (response.IsSuccess)
                return View(response.Content);

            return NotFound(response.ErrorDetails);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAuthors([FromQuery] AuthorRequestFilter filter = null)
        {
            var result = await _authorService
                .GetAuthorsAsync(filter);

            if (result.response.IsSuccess)
            {
                ViewBag.metadata = result.metadata;
                return PartialView("_GetAuthors", result.response.Content);
            }

            return NotFound(result.response.ErrorDetails);
        }

        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.AdminRole}, {RoleConsts.EditorRole}")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateModelContent]
        [Authorize(Roles = $"{RoleConsts.AdminRole}, {RoleConsts.EditorRole}")]
        public async Task<IActionResult> Add([FromForm] AuthorAddDto authorAddDto)
        {
            var response = await _authorService.AddAuthorAsync(authorAddDto);
            if (response.IsSuccess)
                return View();

            return NotFound();
        }

        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.AdminRole}, {RoleConsts.EditorRole}")]
        public async Task<IActionResult> Update([FromRoute] Guid id)
        {
            var response = await _authorService.GetAuthorAsync(id);
            if (response.IsSuccess)
            {
                var authorDto = _mapper.Map<AuthorUpdateDto>(response.Content);
                return View(authorDto);
            }

            return NotFound(response.ErrorDetails);
        }

        [HttpPost]
        [ValidateModelContent]
        [Authorize(Roles = $"{RoleConsts.AdminRole}, {RoleConsts.EditorRole}")]
        public async Task<IActionResult> Update([FromForm] AuthorUpdateDto authorUpdateDto)
        {
            var response = await _authorService.UpdateAuthorAsync(authorUpdateDto);
            if (response.IsSuccess)
                return RedirectToAction(nameof(Index));

            return NotFound();
        }


        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.AdminRole}, {RoleConsts.EditorRole}")]
        public async Task<IActionResult> SafeDelete([FromRoute] Guid id)
        {
            var response = await _authorService.SafeDeleteAuthorAsync(id);
            if (response.IsSuccess)
                return RedirectToAction(nameof(Index));

            return NotFound();
        }
    }
}
