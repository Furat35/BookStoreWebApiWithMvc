using System.Linq.Expressions;
using WebApi.Core.Models.BookGenre;
using WebApi.Core.RequestFilters;
using WebApi.Core.RequestFilters.BookGenre;
using WebApi.Entity.Entities;

namespace WebApi.Service.Abstract
{
    public interface IBookGenreService
    {
        Task<(List<BookGenreDto>, Metadata metadata)> GetBookGenresAsync(
            Expression<Func<BookGenre, bool>> predicate = null, BookGenreRequestFilter filters = null);
        Task<BookGenreDto> GetBookGenreByGuidAsync(Guid id);
        Task<BookGenreDto> GetFirstBookGenreAsync(Expression<Func<BookGenre, bool>> predicate);
        Task<BookGenreDto> AddBookGenreAsync(BookGenreAddDto entity);
        Task DeleteBookGenreAsync(Guid id);
        Task SafeDeleteBookGenreAsync(Guid id);
        Task UpdateBookGenreAsync(BookGenreUpdateDto entity);
        Task<int> CountBookGenresAsync(Expression<Func<BookGenre, bool>> predicate);
    }
}
