using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Entity.Entities;

namespace WebApi.Entity.EntityTypeConfigs
{
    public class AppUserConfigs : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            var users = new List<AppUser>()
            {
                new AppUser
                {
                    Id = Guid.Parse("651E4C92-91AB-4956-9427-3DA18065B9C7"),
                    UserName = "Mehmet",
                    NormalizedUserName = "MEHMET",
                    Email ="mehmet@gmail.com",
                    NormalizedEmail ="MEHMET@GMAIL.COM",
                    SecurityStamp =  "151E4C92-91AB-4956-9427-3DA18065B9C7",
                    ConcurrencyStamp = "331E4C92-91AB-4956-9427-3DA18065B9C7",
                }
            };
            var hasher = new PasswordHasher<AppUser>();
            users[0].PasswordHash = hasher.HashPassword(users[0], "123456");

            builder.HasData(users);
            builder.HasKey(_ => _.Id);
        }
    }
}
