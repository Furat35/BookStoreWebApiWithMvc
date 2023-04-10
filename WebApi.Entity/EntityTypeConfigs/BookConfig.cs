using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Entity.Entities;

namespace WebApi.Entity.EntityTypeConfigs
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(_ => _.Id);

            var books = new List<Book>()
            {
                new Book()
                {
                    Id = Guid.Parse("D132C0A2-4694-4390-84B2-10F7C8CD90FA"),
                    Name = "Kitap1",
                    Summary = "g]zel kitap",
                    Page = 232,
                    AuthorId = new Guid("2496DE42-94EF-4AFD-A575-241510D88294"),
                    PublisherId = new Guid("A0A66353-7E36-457F-B6B0-8BAA1DF6029A"),
                },
                new Book()
                {
                    Id = Guid.Parse("1C2EE0BD-8F81-4CCE-8891-13D28C982455"),
                    Name = "deneme",
                    Summary = "fena degil",
                    Page = 232,
                    AuthorId = Guid.Parse("D129C85A-D830-4D67-BE09-712DC444C794"),
                    PublisherId = Guid.Parse("A0A66353-7E36-457F-B6B0-8BAA1DF6029A"),
                }
            };

            builder.HasData(books);
        }
    }
}
