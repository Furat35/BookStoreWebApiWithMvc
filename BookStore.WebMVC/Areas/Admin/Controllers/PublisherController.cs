using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublisherStore.WebMVC.ApiServices.Abstract;
using WebApi.Core.ActionAttributes;
using WebApi.Core.Consts;
using WebApi.Core.Models.Publisher;
using WebApi.Core.RequestFilters.Publisher;

namespace BookStore.WebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class PublisherController : Controller
    {
        #region Fields
        private readonly IPublisherService _publisherService;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public PublisherController(IPublisherService publisherService, IMapper mapper)
        {
            _publisherService = publisherService;
            _mapper = mapper;
        }
        #endregion

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index([FromQuery] PublisherRequestFilter filter = null)
        {
            var result = await _publisherService
                .GetPublishersAsync(filter);
            if (result.response.IsSuccess)
            {
                ViewBag.metadata = result.metadata;
                return View(result.response.Content);
            }

            return NotFound(result.response.ErrorDetails);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetPublishers([FromQuery] PublisherRequestFilter filter = null)
        {
            var result = await _publisherService
                .GetPublishersAsync(filter);
            if (result.response.IsSuccess)
            {
                ViewBag.metadata = result.metadata;
                return PartialView("_GetPublishers", result.response.Content);
            }

            return NotFound(result.response.ErrorDetails);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetPublisher([FromRoute] Guid id)
        {
            var response = await _publisherService.GetPublisherAsync(id);
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
        public async Task<IActionResult> Add([FromForm] PublisherAddDto publisherAddDto)
        {
            var response = await _publisherService
                .AddPublisherAsync(publisherAddDto);
            if (response.IsSuccess)
                return RedirectToAction(nameof(Index));

            return NotFound();
        }

        [Authorize(Roles = $"{RoleConsts.AdminRole}, {RoleConsts.EditorRole}")]
        public async Task<IActionResult> Update([FromRoute] Guid id)
        {
            var response = await _publisherService.GetPublisherAsync(id);
            if (response.IsSuccess)
            {
                var map = _mapper.Map<PublisherUpdateDto>(response.Content);
                return View(map);
            }

            return NotFound(response.ErrorDetails);
        }

        [HttpPost]
        [ValidateModelContent]
        [Authorize(Roles = $"{RoleConsts.AdminRole}, {RoleConsts.EditorRole}")]
        public async Task<IActionResult> Update([FromForm] PublisherUpdateDto publisherUpdateDto)
        {
            var response = await _publisherService
                .UpdatePublisherAsync(publisherUpdateDto);
            if (response.IsSuccess)
                return RedirectToAction(nameof(Index));

            return NotFound();
        }


        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.AdminRole}, {RoleConsts.EditorRole}")]
        public async Task<IActionResult> SafeDelete([FromRoute] Guid id)
        {
            var response = await _publisherService.SafeDeletePublisherAsync(id);
            if (response.IsSuccess)
                return RedirectToAction(nameof(Index));

            return NotFound();
        }
    }
}
