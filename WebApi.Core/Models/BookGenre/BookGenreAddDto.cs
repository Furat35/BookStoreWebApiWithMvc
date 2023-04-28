using WebApi.Core.Models.Book;
using WebApi.Core.Models.Genres;

namespace WebApi.Core.Models.BookGenre
{
    public record BookGenreAddDto
    {
        public Guid GenresId { get; init; }
        public GenreDto? Genres { get; init; }
        public Guid BookId { get; init; }
        public BookDto? Books { get; init; }
    }
}
