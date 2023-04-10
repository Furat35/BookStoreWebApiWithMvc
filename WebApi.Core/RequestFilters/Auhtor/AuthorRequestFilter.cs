namespace WebApi.Core.RequestFilters.Auhtor
{
    public class AuthorRequestFilter : RequestFilter
    {
        public string? FirstName { get; set; }
        public string? OrderBy { get; set; } = "Id";
    }
}
