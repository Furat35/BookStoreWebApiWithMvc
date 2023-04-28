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
            Expression<Func<Book, bool>> predicate = null, BookRequestFilter filters = null);
        Task<BookDto> GetBookByGuidAsync(Guid id);
        Task<BookDto> AddBookAsync(BookAddDto entity);
        Task SafeDeleteBookAsync(Guid id);
        Task UpdateBookAsync(BookUpdateDto entity);
        //Task<BookDto> GetFirstBookAsync(Expression<Func<Book, bool>> predicate, bool trackChanges = false);
        //Task DeleteBookAsync(Guid id);
        //Task<int> CountBooksAsync(Expression<Func<Book, bool>> predicate);
    }
}
