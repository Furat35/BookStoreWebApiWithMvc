using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Entity.Entities;

namespace WebApi.Entity.EntityTypeConfigs
{
    public class AppRoleConfig : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            var roles = new List<AppRole>
            {
                new AppRole
                {
                    Id = Guid.Parse("1BA776BF-3D61-467A-9002-3A259E5D09D5"),
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "331E4C92-91AB-4956-9427-3DA18065B9C7",
                },
                new AppRole
                {
                    Id = Guid.Parse("BBA776BF-3D61-467A-9002-3A259E5D09D5"),
                    Name = "Editor",
                    NormalizedName = "Editor",
                    ConcurrencyStamp = "331E4C92-91AB-4956-9427-3DA18065B9C7",
                },
                new AppRole
                {
                    Id = Guid.Parse("9BA776BF-3D61-467A-9002-3A259E5D09D5"),
                    Name = "User",
                    NormalizedName = "User",
                    ConcurrencyStamp = "331E4C92-91AB-4956-9427-3DA18065B9C7",
                }
            };
            builder.HasData(roles);
            builder.HasKey(_ => _.Id);
        }
    }
}
