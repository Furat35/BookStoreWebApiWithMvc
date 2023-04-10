using WebApi.Core.Entities.Concrete;

namespace WebApi.Entity.Entities
{
    public class Book : BaseEntity
    {
        public string Name { get; set; }
        public string Summary { get; set; }
        public short Page { get; set; }
        public Guid AuthorId { get; set; }
        public Author? Author { get; set; }
        public Guid PublisherId { get; set; }
        public Publisher? Publisher { get; set; }
        public ICollection<BookGenre>? BookGenres { get; set; }

    }
}
