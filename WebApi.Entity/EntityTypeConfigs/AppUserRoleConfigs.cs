using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApi.Entity.EntityTypeConfigs
{
    public class AppUserRoleConfigs : IEntityTypeConfiguration<IdentityUserRole<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
        {
            builder.HasData(new List<IdentityUserRole<Guid>>()
            {
                new IdentityUserRole<Guid>()
                {
                    RoleId = Guid.Parse("1BA776BF-3D61-467A-9002-3A259E5D09D5"),
                    UserId = Guid.Parse("651E4C92-91AB-4956-9427-3DA18065B9C7")
                },
                //new IdentityUserRole<Guid>()
                //{
                //    RoleId = Guid.Parse("2BA776BF-3D61-467A-9002-3A259E5D09D5"),
                //    UserId = Guid.Parse("151E4C92-91AB-4956-9427-3DA18065B9C7")
                //},
                //new IdentityUserRole<Guid>()
                //{
                //    RoleId = Guid.Parse("3BA776BF-3D61-467A-9002-3A259E5D09D5"),
                //    UserId = Guid.Parse("951E4C92-91AB-4956-9427-3DA18065B9C7")
                //}
            });
        }
    }
}
