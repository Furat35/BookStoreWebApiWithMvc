using WebApi.Core.RequestFilters.Book;
using WebApi.Entity.Entities;

namespace WebApi.Service.Extensions.Filters
{
    public static class BookFilters
    {
        public static IQueryable<Book> GetFilteredBooks(this IQueryable<Book> books, BookRequestFilter filters)
        {
            if (filters is null)
                return books;

            if (filters.MaxPage < filters.MinPage)
            {
                filters.MaxPage = null;
                filters.MinPage = null;
            }

            return books
                .Where(_ =>
                    filters.Name != null
                        ? _.Name.ToUpper().StartsWith(filters.Name.ToUpper())
                        : true)
                .Where(_ =>
                    filters.MinPage != null
                        ? _.Page >= filters.MinPage
                        : true)
                .Where(_ =>
                    filters.MaxPage != null
                        ? _.Page <= filters.MaxPage
                        : true);
        }
    }
}
