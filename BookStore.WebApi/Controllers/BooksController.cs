using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebApi.Core.ActionAttributes;
using WebApi.Core.Consts;
using WebApi.Core.Models.Book;
using WebApi.Core.RequestFilters.Book;
using WebApi.Service.Abstract;
using WebApi.Service.ActionAttributes;

namespace BookStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        #region Fields
        private readonly IBookService _bookService;
        #endregion

        #region Ctor
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }
        #endregion

        [HttpGet]
        [ResponseCache(Duration = 300)]
        [CacheData(Duration = 5)]
        public async Task<IActionResult> GetBooks([FromQuery] BookRequestFilter filters = null)
        {
            var books = await _bookService
                .GetBooksAsync(_ => !_.IsDeleted, filters);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(books.metadata));

            return Ok(books.books);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetBooks([FromRoute] Guid id)
        {
            var book = await _bookService
                .GetBookByGuidAsync(id);
            return Ok(book);
        }

        [HttpPost("[action]")]
        [ValidateModelContent]
        [RemoveCache]
        [Authorize(Roles = $"{RoleConsts.AdminRole}, {RoleConsts.EditorRole}")]
        public async Task<IActionResult> Add([FromBody] BookAddDto bookDto)
        {
            var book = await _bookService
                .AddBookAsync(bookDto);
            return CreatedAtAction(nameof(GetBooks), new { id = book.Id }, book);
        }

        [HttpPost("[action]")]
        [ValidateModelContent]
        [RemoveCache]
        [Authorize(Roles = $"{RoleConsts.AdminRole}, {RoleConsts.EditorRole}")]
        public async Task<IActionResult> Update([FromBody] BookUpdateDto bookDto)
        {
            await _bookService
                .UpdateBookAsync(bookDto);
            return NoContent();
        }

        [HttpPost("[action]/{id:guid}")]
        [RemoveCache]
        [Authorize(Roles = $"{RoleConsts.AdminRole}, {RoleConsts.EditorRole}")]
        public async Task<IActionResult> SafeDelete([FromRoute] Guid id)
        {
            await _bookService
                .SafeDeleteBookAsync(id);
            return NoContent();
        }
    }
}
