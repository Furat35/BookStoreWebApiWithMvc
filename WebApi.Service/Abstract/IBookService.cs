using System.Linq.Expressions;
using WebApi.Core.Models.Book;
using WebApi.Core.RequestFilters;
using WebApi.Core.RequestFilters.Book;
using WebApi.Entity.Entities;

namespace WebApi.Service.Abstract
{
    public interface IBookService
    {
        Task<(List<BookDto> books, Metadata metadata)> GetBooksAsync(
            Expression<Func<Book, bool>> predicate = null, BookRequestFilter filters = null, bool trackChanges = false);
        Task<BookDto> GetBookByGuidAsync(Guid id);
        Task<BookDto> GetFirstBookAsync(Expression<Func<Book, bool>> predicate, bool trackChanges = false);
        Task<BookDto> AddBookAsync(BookAddDto entity);
        Task DeleteBookAsync(Guid id);
        Task SafeDeleteBookAsync(Guid id);
        Task UpdateBookAsync(BookUpdateDto entity);
        Task<int> CountBooksAsync(Expression<Func<Book, bool>> predicate);
    }
}
