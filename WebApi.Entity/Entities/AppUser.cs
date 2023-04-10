using Microsoft.AspNetCore.Identity;

namespace WebApi.Entity.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public bool IsDeleted { get; set; }
    }
}
