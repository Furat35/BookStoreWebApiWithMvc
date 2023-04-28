using AutoMapper;
using BookStore.WebMVC.ApiServices.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Core.ActionAttributes;
using WebApi.Core.Consts;
using WebApi.Core.Models.Book;
using WebApi.Core.RequestFilters.Book;

namespace BookStore.WebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class BookController : Controller
    {
        #region Fields
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public BookController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }
        #endregion

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index([FromQuery] BookRequestFilter filter = null)
        {
            var result = await _bookService
                .GetBooksAsync(filter);
            if (result.response.IsSuccess)
            {
                ViewBag.metadata = result.metadata;
                return View(result.response.Content);
            }

            return NotFound(result.response.ErrorDetails);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetBooks([FromQuery] BookRequestFilter filter = null)
        {
            var result = await _bookService
                .GetBooksAsync(filter);

            if (result.response.IsSuccess)
            {
                ViewBag.metadata = result.metadata;
                return PartialView("_GetBooks", result.response.Content);
            }

            return NotFound(result.response.ErrorDetails);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetBook([FromRoute] Guid id)
        {
            var response = await _bookService
                .GetBookAsync(id);
            if (response.IsSuccess)
                return View(response.Content);

            return NotFound(response.ErrorDetails);
        }

        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.AdminRole}, {RoleConsts.EditorRole}")]
        public async Task<IActionResult> Add()
        {
            var bookDto = await _bookService
                .AddBookIncludeRelations();
            var map = _mapper.Map<BookAddDto>(bookDto);
            return View(map);
        }

        [HttpPost]
        [ValidateModelContent]
        [Authorize(Roles = $"{RoleConsts.AdminRole}, {RoleConsts.EditorRole}")]
        public async Task<IActionResult> Add([FromForm] BookAddDto bookAddDto)
        {
            var response = await _bookService
                .AddBookAsync(bookAddDto);
            if (response.IsSuccess)
                return RedirectToAction(nameof(Index));

            return NotFound();
        }

        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.AdminRole}, {RoleConsts.EditorRole}")]
        public async Task<IActionResult> Update([FromRoute] Guid id)
        {
            var bookDto = await _bookService
                .UpdateBookIncludeRelations(id);
            return View(bookDto);
        }

        [HttpPost]
        [ValidateModelContent]
        [Authorize(Roles = $"{RoleConsts.AdminRole}, {RoleConsts.EditorRole}")]
        public async Task<IActionResult> Update([FromForm] BookUpdateDto bookUpdateDto)
        {
            var response = await _bookService
                .UpdateBookAsync(bookUpdateDto);
            if (response.IsSuccess)
                return RedirectToAction(nameof(Index));

            return NotFound();
        }


        [HttpGet]
        [Authorize(Roles = $"{RoleConsts.AdminRole}, {RoleConsts.EditorRole}")]
        public async Task<IActionResult> SafeDelete([FromRoute] Guid id)
        {
            var response = await _bookService
                .SafeDeleteBookAsync(id);
            if (response.IsSuccess)
                return RedirectToAction(nameof(Index));

            return NotFound();
        }
    }
}
