using WebApi.Core.Models.BookGenre;

namespace WebApi.Core.Models.Book
{
    public record BookUpdateDto
    {
        public BookUpdateDto()
        {

        }
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Summary { get; init; }
        public short Page { get; init; }
        public Guid AuthorId { get; init; }
        public Guid PublisherId { get; init; }
        public ICollection<BookGenreDto>? BookGenres { get; set; }
    }
}
