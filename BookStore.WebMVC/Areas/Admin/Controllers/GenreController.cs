using AutoMapper;
using GenreStore.WebMVC.ApiServices.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Core.ActionAttributes;
using WebApi.Core.Consts;
using WebApi.Core.Models.Genres;
using WebApi.Core.RequestFilters.Genre;

namespace BookStore.WebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class GenreController : Controller
    {
        #region Fields
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public GenreController(IGenreService genreService, IMapper mapper)
        {
            _genreService = genreService;
            _mapper = mapper;
        }
        #endregion

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index([FromQuery] GenreRequestFilter filter = null)
        {
            var result = await _genreService
                .GetGenresAsync(filter);
            if (result.response.IsSuccess)
            {
                ViewBag.metadata = result.metadata;
                return View(result.response.Content);
            }

            return NotFound(result.response.ErrorDetails);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetGenres([FromQuery] GenreRequestFilter filter = null)
        {
            var result = await _genreService
                .GetGenresAsync(filter);

            if (result.response.IsSuccess)
            {
                ViewBag.metadata = result.metadata;
                return PartialView("_GetBooks", result.response.Content);
            }

            return NotFound(result.response.ErrorDetails);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetGenre([FromRoute] Guid id)
        {
            var response = await _genreService.GetGenreAsync(id);
            if (response.IsSuccess)
                return View(response.Content);

            return NotFound(response.ErrorDetails);
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
        public async Task<IActionResult> Add([FromForm] GenreAddDto genreAddDto)
        {
            var response = await _genreService.AddGenreAsync(genreAddDto);
            if (response.IsSuccess)
                return RedirectToAction(nameof(Index));

            return NotFound();
        }

        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.AdminRole}, {RoleConsts.EditorRole}")]
        public async Task<IActionResult> Update([FromRoute] Guid id)
        {
            var response = await _genreService.GetGenreAsync(id);
            if (response.IsSuccess)
            {
                var map = _mapper.Map<GenreUpdateDto>(response.Content);
                return View(map);
            }

            return NotFound(response.ErrorDetails);
        }

        [HttpPost]
        [ValidateModelContent]
        [Authorize(Roles = $"{RoleConsts.AdminRole}, {RoleConsts.EditorRole}")]
        public async Task<IActionResult> Update([FromForm] GenreUpdateDto genreUpdateDto)
        {
            var response = await _genreService.UpdateGenreAsync(genreUpdateDto);
            if (response.IsSuccess)
                return RedirectToAction(nameof(Index));

            return NotFound();
        }


        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.AdminRole}, {RoleConsts.EditorRole}")]
        public async Task<IActionResult> SafeDelete([FromRoute] Guid id)
        {
            var response = await _genreService.SafeDeleteGenreAsync(id);
            if (response.IsSuccess)
                return RedirectToAction(nameof(Index));

            return NotFound();
        }
    }
}
