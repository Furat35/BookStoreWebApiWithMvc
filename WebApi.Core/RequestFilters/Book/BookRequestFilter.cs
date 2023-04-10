namespace WebApi.Core.RequestFilters.Book
{
    public class BookRequestFilter : RequestFilter
    {
        public string? Name { get; set; }
        public int? MinPage { get; set; }
        public int? MaxPage { get; set; }
        //public string? Author { get; set; }
        //public string? Genre { get; set; }
        //public string? Publisher { get; set; }
    }
}
