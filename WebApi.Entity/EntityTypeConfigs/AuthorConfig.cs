using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Entity.Entities;

namespace WebApi.Entity.EntityTypeConfigs
{
    public class AuthorConfig : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(_ => _.Id);

            var authors = new List<Author>()
            {
                new Author()
                {
                    Id = Guid.Parse("2496DE42-94EF-4AFD-A575-241510D88294"),
                    FirstName = "firat",
                    LastName = "ortac",
                    Phone = "5375626252",
                    Mail = "furat__@hotmail.com",
                    BirthDate = new DateTime(2000,10,20),
                    CreateDate = DateTime.Now,
                    IsDeleted = false
                },
                new Author()
                {
                    Id = Guid.Parse("D129C85A-D830-4D67-BE09-712DC444C794"),
                    FirstName = "Merve",
                    LastName = "ortac",
                    Phone = "5375626252",
                    Mail = "merve__@hotmail.com",
                    BirthDate = new DateTime(2011,6,5),
                    CreateDate = DateTime.Now,
                    IsDeleted = false
                }
            };

            builder.HasData(authors);
        }
    }
}
