using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Entity.Entities;

namespace WebApi.Entity.EntityTypeConfigs
{
    public class GenresConfig : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasKey(_ => _.Id);

            var genres = new List<Genre>()
            {
                new Genre
                {
                    Id = Guid.Parse("ECDD8F0A-5F41-41CE-B732-F269A453B110"),
                    Name = "Korku"
                }
            };

            builder.HasData(genres);
        }
    }
}
