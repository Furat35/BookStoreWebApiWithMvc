using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Core.ActionAttributes;
using WebApi.Core.Models.BookGenre;
using WebApi.Core.RequestFilters.BookGenre;
using WebApi.Service.Abstract;

namespace BookGenreStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookGenresController : ControllerBase
    {
        private readonly IBookGenreService _bookService;

        public BookGenresController(IBookGenreService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> BookGenres([FromQuery] BookGenreRequestFilter filters = null)
        {
            var books = await _bookService
                .GetBookGenresAsync(_ => !_.IsDeleted, filters, false);
            return Ok(books);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> BookGenres([FromRoute] Guid id)
        {
            var book = await _bookService
                .GetBookGenreByGuidAsync(id);
            return Ok(book);
        }

        [HttpPost("[action]")]
        [ValidateModelContent]
        public async Task<IActionResult> Add([FromBody] BookGenreAddDto bookDto)
        {
            var bookGenre = _bookService
                .AddBookGenreAsync(bookDto);
            return CreatedAtRoute(nameof(BookGenres), new { id = bookGenre.Id }, bookGenre);
        }

        [HttpPost("[action]")]
        [ValidateModelContent]
        public async Task<IActionResult> Update([FromBody] BookGenreUpdateDto bookDto)
        {
            await _bookService
                .UpdateBookGenreAsync(bookDto);
            return NoContent();
        }

        [HttpPost("[action]/{id:guid}")]
        public async Task<IActionResult> SafeDelete([FromRoute] Guid id)
        {
            await _bookService
                .SafeDeleteBookGenreAsync(id);
            return NoContent();
        }
    }
}
