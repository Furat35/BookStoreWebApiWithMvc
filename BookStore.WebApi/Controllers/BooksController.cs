﻿using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebApi.Core.ActionAttributes;
using WebApi.Core.Models.Book;
using WebApi.Core.RequestFilters.Book;
using WebApi.Entity.Entities;
using WebApi.Service.Abstract;

namespace BookStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BooksController> _logger;
        public BooksController(IBookService bookService, ILogger<BooksController> logger, IValidator<Book> validator)
        {
            _bookService = bookService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Books([FromQuery] BookRequestFilter filters = null)
        {
            var books = await _bookService.GetBooksAsync(_ => !_.IsDeleted, filters, false);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(books.metadata));

            return Ok(books.books);
        }

        [HttpGet("[action]/{id:guid}")]
        public async Task<IActionResult> Book([FromRoute] Guid id)
        {
            var book = await _bookService.GetBookByGuidAsync(id);
            return Ok(book);
        }

        [HttpPost("[action]")]
        [ValidateModelContent]
        public async Task<IActionResult> Add([FromBody] BookAddDto bookDto)
        {
            var book = await _bookService.AddBookAsync(bookDto);
            return CreatedAtAction(nameof(Book), new { id = book.Id }, book);
        }

        [HttpPost("[action]")]
        [ValidateModelContent]
        public async Task<IActionResult> Update([FromBody] BookUpdateDto bookDto)
        {
            await _bookService.UpdateBookAsync(bookDto);
            return NoContent();
        }

        [HttpPost("[action]/{id:guid}")]
        public async Task<IActionResult> SafeDelete([FromRoute] Guid id)
        {
            await _bookService.SafeDeleteBookAsync(id);
            return NoContent();
        }
    }
}
