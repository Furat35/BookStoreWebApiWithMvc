using WebApi.Core.RequestFilters.Publisher;
using WebApi.Entity.Entities;

namespace WebApi.Service.Extensions.Filters
{
    public static class PublisherFilters
    {
        public static IQueryable<Publisher> GetFilteredPublishers(this IQueryable<Publisher> books, PublisherRequestFilter filters)
        {
            return books
                .Where(_ =>
                    filters.PublisherName != null
                        ? _.PublisherName.ToUpper().StartsWith(filters.PublisherName.ToUpper())
                        : true);
        }
    }
}
