using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebApi.Core.ActionAttributes;
using WebApi.Core.Consts;
using WebApi.Core.Models.Author;
using WebApi.Core.RequestFilters.Auhtor;
using WebApi.Service.Abstract;
using WebApi.Service.ActionAttributes;

namespace BookStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        #region Fields
        private readonly IAuthorService _authorService;
        #endregion

        #region Ctor
        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        #endregion

        [HttpGet]
        [ResponseCache(Duration = 300)]
        [CacheData(Duration = 5)]
        public async Task<IActionResult> GetAuthors([FromQuery] AuthorRequestFilter filters = null)
        {
            var response = await _authorService
                .GetAuthorsAsync(_ => !_.IsDeleted, filters);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(response.metadata));

            return Ok(response.authors);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAuthors([FromRoute] Guid id)
        {
            var author = await _authorService
                .GetAuthorByGuidAsync(id);
            return Ok(author);
        }

        [HttpPost("[action]")]
        [ValidateModelContent]
        [RemoveCache]
        [Authorize(Roles = $"{RoleConsts.AdminRole}, {RoleConsts.EditorRole}")]
        public async Task<IActionResult> Add([FromBody] AuthorAddDto authorDto)
        {
            var author = await _authorService
                .AddAuthorAsync(authorDto);
            return CreatedAtAction(nameof(GetAuthors), new { id = author.Id }, author);
        }

        [HttpPost("[action]")]
        [ValidateModelContent]
        [RemoveCache]
        [Authorize(Roles = $"{RoleConsts.AdminRole}, {RoleConsts.EditorRole}")]
        public async Task<IActionResult> Update([FromBody] AuthorUpdateDto authorDto)
        {
            await _authorService
                .UpdateAuthorAsync(authorDto);
            return NoContent();
        }

        [HttpPost("[action]/{id:guid}")]
        [RemoveCache]
        [Authorize(Roles = $"{RoleConsts.AdminRole}, {RoleConsts.EditorRole}")]
        public async Task<IActionResult> SafeDelete([FromRoute] Guid id)
        {
            await _authorService
                .SafeDeleteAuthorAsync(id);
            return NoContent();
        }
    }
}
