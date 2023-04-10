using WebApi.Core.RequestFilters.Genre;
using WebApi.Entity.Entities;

namespace WebApi.Service.Extensions.Filters
{
    public static class GenreFilters
    {
        public static IQueryable<Genre> GetFilteredBooks(this IQueryable<Genre> genres, GenreRequestFilter filters)
        {
            return genres
                .Where(_ =>
                    filters.Name != null
                        ? _.Name.ToUpper().StartsWith(filters.Name.ToUpper())
                        : true);
            ;
        }
    }
}
