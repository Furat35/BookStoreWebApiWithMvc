namespace WebApi.Core.Models.Author
{
    public record AuthorAddDto
    {
        public AuthorAddDto()
        {

        }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string? Phone { get; init; }
        public string? Mail { get; init; }
        public DateTime? BirthDate { get; init; } = DateTime.Now;
    }
}
