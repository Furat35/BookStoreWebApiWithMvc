namespace WebApi.Core.RequestFilters.Book
{
    public class BookRequestFilter : RequestFilter
    {
        public string? Name { get; set; }
        public int? MinPage { get; set; }
        public int? MaxPage { get; set; }
        public bool? IncludeEntities { get; set; }
    }
}
