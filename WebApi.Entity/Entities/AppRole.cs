using Microsoft.AspNetCore.Identity;

namespace WebApi.Entity.Entities
{
    public class AppRole : IdentityRole<Guid>
    {
        public bool IsDeleted { get; set; }
    }
}
