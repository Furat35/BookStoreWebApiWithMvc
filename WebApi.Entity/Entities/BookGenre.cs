using WebApi.Core.Entities.Concrete;

namespace WebApi.Entity.Entities
{
    public class BookGenre : BaseEntity
    {
        public Guid GenresId { get; set; }
        public Genre? Genres { get; set; }
        public Guid BookId { get; set; }
        public Book? Books { get; set; }
    }
}
