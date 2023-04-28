using WebApi.Core.RequestFilters.Auhtor;
using WebApi.Entity.Entities;

namespace WebApi.Service.Extensions.Filters
{
    public static class AuthorFilters
    {
        public static IQueryable<Author> GetFilterAuthors(this IQueryable<Author> author, AuthorRequestFilter filters)
        {
            if (filters is null)
                return author;
            return author
                .Where(_ =>
                    filters.FirstName != null
                        ? _.FirstName.ToUpper().StartsWith(filters.FirstName.ToUpper())
                        : true);

        }

        //public static IQueryable<Author> OrderAuthors(this IQueryable<Author> author, AuthorRequestFilter filters)
        //{
        //    PropertyInfo? orderByProperty = null;
        //    if (filters.OrderBy != "Id")
        //    {
        //        bool validOrder = false;
        //        var filterOrderBy = typeof(AuthorRequestFilter).GetProperty(nameof(filters.OrderBy));
        //        string orderBy = filterOrderBy.GetValue(filters).ToString();
        //        var authorProperties = typeof(Author).GetProperties();


        //        foreach (var property in authorProperties)
        //        {
        //            if (property.Name.ToUpper() == orderBy.ToUpper())
        //            {
        //                validOrder = true;
        //                orderByProperty = property;
        //                break;
        //            }
        //        }

        //        if (!validOrder)
        //        {
        //            orderByProperty = typeof(Author).GetProperty("Id");
        //        }

        //    }
        //}
    }
}
