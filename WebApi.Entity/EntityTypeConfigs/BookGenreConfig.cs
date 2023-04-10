using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Entity.Entities;

namespace WebApi.Entity.EntityTypeConfigs
{
    public class BookGenreConfig : IEntityTypeConfiguration<BookGenre>
    {
        public void Configure(EntityTypeBuilder<BookGenre> builder)
        {
            builder.HasKey(_ => _.Id);

            var bookGenres = new List<BookGenre>()
            {
                new BookGenre()
                {
                    Id = Guid.Parse("B36DC350-66C5-4567-BC58-572D1821B7B8"),
                    GenresId = Guid.Parse("ECDD8F0A-5F41-41CE-B732-F269A453B110"),
                    BookId = Guid.Parse("D132C0A2-4694-4390-84B2-10F7C8CD90FA")
                }
            };

            builder.HasData(bookGenres);
        }
    }
}
