using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Entity.Entities;

namespace WebApi.Entity.EntityTypeConfigs
{
    public class PublisherConfig : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.HasKey(_ => _.Id);

            var publishers = new List<Publisher>()
            {
                new Publisher()
                {
                    Id = Guid.Parse("A0A66353-7E36-457F-B6B0-8BAA1DF6029A"),
                    PublisherName = "Ahmet Demir"
                }
            };

            builder.HasData(publishers);
        }
    }
}
