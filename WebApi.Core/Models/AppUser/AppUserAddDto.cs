namespace WebApi.Core.Models.AppUser
{
    public record AppUserAddDto
    {
        public AppUserAddDto()
        {

        }
        public string UserName { get; init; }
        public string Email { get; init; }
        public string? PhoneNumber { get; init; }
        public string Password { get; init; }
    }
}
