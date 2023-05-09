using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApi.Core.Exceptions;
using WebApi.Core.Exceptions.Book;
using WebApi.Core.Models.Book;
using WebApi.Core.RequestFilters;
using WebApi.Core.RequestFilters.Book;
using WebApi.DataAccess.Abstract;
using WebApi.DataAccess.UnitOfWorks;
using WebApi.Entity.Entities;
using WebApi.Service.Abstract;
using WebApi.Service.Extensions.Filters;

namespace WebApi.Service.Concrete
{
    public sealed class BookService : IBookService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<Book> _validator;
        private readonly IRepository<Book> _bookRepo;
        private readonly string _cacheName = $"{typeof(BookService).FullName}.{nameof(GetBooksAsync)}";
        #endregion

        #region Ctor
        public BookService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<Book> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
            _bookRepo = _unitOfWork.GetRepository<Book>();
        }
        #endregion 

        public async Task<(List<BookDto> books, Metadata metadata)> GetBooksAsync(
            Expression<Func<Book, bool>> predicate = null, BookRequestFilter filters = null)
        {
            var books = _bookRepo.GetAllAsync(predicate)
                .Include(_ => _.Author)
                .Include(_ => _.Publisher)
                .Include(_ => _.BookGenres)
                .GetFilteredBooks(filters);

            var bookSkip = (filters.Page - 1) * filters.PageSize;
            bool isValidPage = books.Count() > bookSkip
                ? true
                : false;

            if (!isValidPage)
                throw new InvalidPageException();


            var filteredBooks = await books
                .Skip(bookSkip)
                .Take(filters.PageSize)
                .ToListAsync();


            Metadata metadata = new Metadata()
            {
                CurrentPage = filters.Page,
                PageSize = filters.PageSize,
                TotalPages = books.Count() / filters.PageSize,
                TotalEntities = books.Count()
            };

            var response = filteredBooks != null
                ? (_mapper.Map<List<BookDto>>(filteredBooks), metadata)
                : (null, metadata);

            return response;
        }

        public async Task<BookDto> GetBookByGuidAsync(Guid id)
        {
            var book = await BookExistsAsync(id);
            book.Author = await _unitOfWork.GetRepository<Author>().GetByGuidAsync(book.AuthorId);
            book.Publisher = await _unitOfWork.GetRepository<Publisher>().GetByGuidAsync(book.PublisherId);
            book.BookGenres = await _unitOfWork.GetRepository<BookGenre>().GetAllAsync(_ => _.BookId == id).ToListAsync();

            return _mapper.Map<BookDto>(book);
        }

        public async Task<BookDto> AddBookAsync(BookAddDto entity)
        {
            var book = _mapper.Map<Book>(entity);
            await BookValidatorAsync(book);
            _bookRepo.Add(book);
            await SaveBookAsync();

            return _mapper.Map<BookDto>(book);
        }

        public async Task SafeDeleteBookAsync(Guid id)
        {
            var book = await BookExistsAsync(id);
            book.IsDeleted = true;
            _bookRepo.SafeDelete(book);
            await SaveBookAsync();
        }

        public async Task UpdateBookAsync(BookUpdateDto entity)
        {
            var map = _mapper.Map<Book>(entity);
            await BookValidatorAsync(map);
            var book = await BookExistsAsync(entity.Id);
            _mapper.Map(entity, book);
            _bookRepo.Update(book);
            await SaveBookAsync();
        }

        #region Private Methods
        private async Task SaveBookAsync()
        {
            int effectedRows;
            try
            {
                effectedRows = await _unitOfWork.SaveAsync();
            }
            catch (Exception e)
            {
                throw new BookInternalServerError500Exception(e.Message);
            }

            if (effectedRows != 1)
                throw new BookInternalServerError500Exception();
        }

        private async Task BookValidatorAsync(Book book)
        {
            var result = await _validator.ValidateAsync(book);
            if (!result.IsValid)
                throw new UnprocessableBookException(string.Join(", ", result.Errors.ConvertAll(_ => _.ToString())));
        }

        private async Task<Book> BookExistsAsync(Guid id)
        {
            var book = await _bookRepo.GetByGuidAsync(id);
            if (book is null)
                throw new BookNotFoundException(id);

            return book;
        }
        #endregion

        #region Functions that can be needed later
        //public async Task<BookDto> GetFirstBookAsync(Expression<Func<Book, bool>> predicate, bool trackChanges = false)
        //{
        //    var book = await _bookRepo.GetFirstAsync(predicate, trackChanges);
        //    return book is null
        //        ? _mapper.Map<BookDto>(book)
        //        : null;
        //}

        //public async Task DeleteBookAsync(Guid id)
        //{
        //    var book = await BookExistsAsync(id);
        //    _bookRepo.Delete(book);
        //    await SaveBookAsync();
        //}

        //public async Task<int> CountBooksAsync(Expression<Func<Book, bool>> predicate)
        //{
        //    return await _bookRepo
        //        .CountAsync(predicate);
        //}
        #endregion
    }
}
