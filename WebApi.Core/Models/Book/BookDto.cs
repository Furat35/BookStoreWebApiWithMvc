using WebApi.Core.Models.Author;
using WebApi.Core.Models.BookGenre;
using WebApi.Core.Models.Publisher;

namespace WebApi.Core.Models.Book
{
    public record BookDto
    {
        public BookDto()
        {

        }
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Summary { get; init; }
        public short Page { get; init; }
        public Guid AuthorId { get; init; }
        public AuthorDto? Author { get; init; }
        public Guid PublisherId { get; init; }
        public PublisherDto? Publisher { get; init; }
        public ICollection<BookGenreDto>? BookGenres { get; init; }
    }
}
