using WebApi.Core.Models.Book;
using WebApi.Core.Models.Genres;

namespace WebApi.Core.Models.BookGenre
{
    public class BookGenreAddDto
    {
        public Guid GenresId { get; set; }
        public GenreDto? Genres { get; set; }
        public Guid BookId { get; set; }
        public BookDto? Books { get; set; }
    }
}
