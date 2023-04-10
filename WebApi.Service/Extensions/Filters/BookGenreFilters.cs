using WebApi.Core.RequestFilters.BookGenre;
using WebApi.Entity.Entities;

namespace WebApi.Service.Extensions.Filters
{
    public static class BookGenreFilters
    {
        public static IQueryable<BookGenre> GetFilteredBookGenres(this IQueryable<BookGenre> books, BookGenreRequestFilter filters)
        {
            return books;
        }

    }
}
