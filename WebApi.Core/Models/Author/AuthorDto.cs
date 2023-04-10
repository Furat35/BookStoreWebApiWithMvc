using WebApi.Core.Models.Book;

namespace WebApi.Core.Models.Author
{
    public record AuthorDto
    {
        public AuthorDto()
        {

        }
        public Guid Id { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string? Phone { get; init; }
        public string? Mail { get; init; }
        public DateTime? BirthDate { get; init; }
        public ICollection<BookDto>? Books { get; init; }
    }
}
