using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebApi.Core.ActionAttributes;
using WebApi.Core.Consts;
using WebApi.Core.Models.Publisher;
using WebApi.Core.RequestFilters.Publisher;
using WebApi.Service.Abstract;
using WebApi.Service.ActionAttributes;

namespace PublisherStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        #region Fields
        private readonly IPublisherService _publisherService;
        #endregion

        #region Ctor
        public PublishersController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }
        #endregion

        [HttpGet]
        [ResponseCache(Duration = 300)]
        [CacheData(Duration = 5)]
        public async Task<IActionResult> GetPublishers([FromQuery] PublisherRequestFilter filters = null)
        {
            var result = await _publisherService
                .GetPublishersAsync(_ => !_.IsDeleted, filters);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.metadata));

            return Ok(result.publishers);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetPublishers([FromRoute] Guid id)
        {
            var book = await _publisherService
                .GetPublisherByGuidAsync(id);
            return Ok(book);
        }

        [HttpPost("[action]")]
        [ValidateModelContent]
        [RemoveCache]
        [Authorize(Roles = $"{RoleConsts.AdminRole}, {RoleConsts.EditorRole}")]
        public async Task<IActionResult> Add([FromBody] PublisherAddDto bookDto)
        {
            var publisher = await _publisherService
                .AddPublisherAsync(bookDto);
            return CreatedAtAction(nameof(GetPublishers), new { id = publisher.Id }, publisher);
        }

        [HttpPost("[action]")]
        [ValidateModelContent]
        [RemoveCache]
        [Authorize(Roles = $"{RoleConsts.AdminRole}, {RoleConsts.EditorRole}")]
        public async Task<IActionResult> Update([FromBody] PublisherUpdateDto bookDto)
        {
            await _publisherService
                .UpdatePublisherAsync(bookDto);
            return NoContent();
        }

        [HttpPost("[action]/{id:guid}")]
        [RemoveCache]
        [Authorize(Roles = $"{RoleConsts.AdminRole}, {RoleConsts.EditorRole}")]
        public async Task<IActionResult> SafeDelete([FromRoute] Guid id)
        {
            await _publisherService
                .SafeDeletePublisherAsync(id);
            return NoContent();
        }
    }
}
