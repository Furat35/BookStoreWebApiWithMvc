using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebApi.Core.ActionAttributes;
using WebApi.Core.Consts;
using WebApi.Core.Models.Genres;
using WebApi.Core.RequestFilters.Genre;
using WebApi.Service.Abstract;
using WebApi.Service.ActionAttributes;

namespace BookStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        #region Fields
        private readonly IGenreService _genreService;
        #endregion

        #region Ctor
        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }
        #endregion

        [HttpGet]
        [ResponseCache(Duration = 300)]
        [CacheData(Duration = 5)]
        public async Task<IActionResult> GetGenres([FromQuery] GenreRequestFilter filters = null)
        {
            var books = await _genreService
                .GetGenresAsync(_ => !_.IsDeleted, filters);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(books.metadata));

            return Ok(books.genres);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetGenres([FromRoute] Guid id)
        {
            var author = await _genreService
                .GetGenreByGuidAsync(id);
            return Ok(author);
        }

        [HttpPost("[action]")]
        [ValidateModelContent]
        [RemoveCache]
        [Authorize(Roles = $"{RoleConsts.AdminRole}, {RoleConsts.EditorRole}")]
        public async Task<IActionResult> Add([FromBody] GenreAddDto authorDto)
        {
            var genre = await _genreService
                .AddGenreAsync(authorDto);
            return CreatedAtAction(nameof(GetGenres), new { id = genre.Id }, genre);
        }

        [HttpPost("[action]")]
        [ValidateModelContent]
        [RemoveCache]
        [Authorize(Roles = $"{RoleConsts.AdminRole}, {RoleConsts.EditorRole}")]
        public async Task<IActionResult> Update([FromBody] GenreUpdateDto authorDto)
        {
            await _genreService
                .UpdateGenreAsync(authorDto);
            return NoContent();
        }

        [HttpPost("[action]/{id:guid}")]
        [RemoveCache]
        [Authorize(Roles = $"{RoleConsts.AdminRole}, {RoleConsts.EditorRole}")]
        public async Task<IActionResult> SafeDelete([FromRoute] Guid id)
        {
            await _genreService
                .SafeDeleteGenreAsync(id);
            return NoContent();
        }
    }
}
