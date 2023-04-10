using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebApi.Core.ActionAttributes;
using WebApi.Core.Models.Genres;
using WebApi.Core.RequestFilters.Genre;
using WebApi.Service.Abstract;

namespace BookStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<IActionResult> Genres([FromQuery] GenreRequestFilter filters = null)
        {
            var books = await _genreService
                .GetGenresAsync(_ => !_.IsDeleted, filters, false);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(books.metadata));

            return Ok(books.genres);
        }

        [HttpGet("[action]/{id:guid}")]
        public async Task<IActionResult> Genre([FromRoute] Guid id)
        {
            var author = await _genreService
                .GetGenreByGuidAsync(id);
            return Ok(author);
        }

        [HttpPost("[action]")]
        [ValidateModelContent]
        public async Task<IActionResult> Add([FromBody] GenreAddDto authorDto)
        {
            var genre = await _genreService
                .AddGenreAsync(authorDto);
            return CreatedAtAction(nameof(Genre), new { id = genre.Id }, genre);
        }

        [HttpPost("[action]")]
        [ValidateModelContent]
        public async Task<IActionResult> Update([FromBody] GenreUpdateDto authorDto)
        {
            await _genreService
                .UpdateGenreAsync(authorDto);
            return NoContent();
        }

        [HttpPost("[action]/{id:guid}")]
        public async Task<IActionResult> SafeDelete([FromRoute] Guid id)
        {
            await _genreService
                .SafeDeleteGenreAsync(id);
            return NoContent();
        }
    }
}
