using WebApi.Core.Entities.Concrete;

namespace WebApi.Entity.Entities
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<BookGenre>? BookGenres { get; set; }

    }
}
