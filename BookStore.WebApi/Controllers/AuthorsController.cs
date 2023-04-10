using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebApi.Core.ActionAttributes;
using WebApi.Core.Models.Author;
using WebApi.Core.RequestFilters.Auhtor;
using WebApi.Service.Abstract;

namespace BookStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> Authors([FromQuery] AuthorRequestFilter filters = null)
        {
            var books = await _authorService
                .GetAuthorsAsync(_ => !_.IsDeleted, filters, false);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(books.metadata));

            return Ok(books.authors);
        }

        [HttpGet("[action]/{id:guid}")]
        public async Task<IActionResult> Author([FromRoute] Guid id)
        {
            var author = await _authorService.GetAuthorByGuidAsync(id);
            return Ok(author);
        }

        [HttpPost("[action]")]
        [ValidateModelContent]
        public async Task<IActionResult> Add([FromBody] AuthorAddDto authorDto)
        {
            var author = await _authorService
                .AddAuthorAsync(authorDto);
            return CreatedAtAction(nameof(Author), new { id = author.Id }, author);
        }

        [HttpPost("[action]")]
        //[ValidateModelContent]
        public async Task<IActionResult> Update([FromBody] AuthorUpdateDto authorDto)
        {
            await _authorService
                .UpdateAuthorAsync(authorDto);
            return NoContent();
        }

        [HttpPost("[action]/{id:guid}")]
        public async Task<IActionResult> SafeDelete([FromRoute] Guid id)
        {
            await _authorService
                .SafeDeleteAuthorAsync(id);
            return NoContent();
        }
    }
}
