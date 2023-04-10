using WebApi.Core.Models.Author;
using WebApi.Core.Models.BookGenre;
using WebApi.Core.Models.Publisher;

namespace WebApi.Core.Models.Book
{
    public record BookAddDto
    {
        public BookAddDto()
        {

        }
        public string Name { get; init; }
        public string Summary { get; init; }
        public short Page { get; init; }
        public Guid AuthorId { get; set; }
        public AuthorDto? Author { get; set; }
        public Guid PublisherId { get; set; }
        public PublisherDto? Publisher { get; set; }
        public ICollection<BookGenreDto>? BookGenres { get; set; }

    }
}
