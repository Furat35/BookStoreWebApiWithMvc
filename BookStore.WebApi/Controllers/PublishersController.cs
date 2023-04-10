using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebApi.Core.ActionAttributes;
using WebApi.Core.Models.Publisher;
using WebApi.Core.RequestFilters.Publisher;
using WebApi.Service.Abstract;

namespace PublisherStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PublishersController : ControllerBase
    {
        private readonly IPublisherService _publisherService;
        public PublishersController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpGet]
        public async Task<IActionResult> Publishers([FromQuery] PublisherRequestFilter filters = null)
        {
            var books = await _publisherService
                .GetPublishersAsync(_ => !_.IsDeleted, filters, false);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(books.metadata));

            return Ok(books.publishers);
        }

        [HttpGet("[action]/{id:guid}")]
        public async Task<IActionResult> Publisher([FromRoute] Guid id)
        {
            var book = await _publisherService
                .GetPublisherByGuidAsync(id);
            return Ok(book);
        }

        [HttpPost("[action]")]
        [ValidateModelContent]
        public async Task<IActionResult> Add([FromBody] PublisherAddDto bookDto)
        {
            var publisher = await _publisherService
                .AddPublisherAsync(bookDto);
            return CreatedAtAction(nameof(Publisher), new { id = publisher.Id }, publisher);
        }

        [HttpPost("[action]")]
        [ValidateModelContent]
        public async Task<IActionResult> Update([FromBody] PublisherUpdateDto bookDto)
        {
            await _publisherService
                .UpdatePublisherAsync(bookDto);
            return NoContent();
        }

        [HttpPost("[action]/{id:guid}")]
        public async Task<IActionResult> SafeDelete([FromRoute] Guid id)
        {
            await _publisherService
                .SafeDeletePublisherAsync(id);
            return NoContent();
        }
    }
}
