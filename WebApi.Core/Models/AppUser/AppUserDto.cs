namespace WebApi.Core.Models.AppUser
{
    public record AppUserDto
    {
        public AppUserDto()
        {

        }
        public Guid Id { get; init; }
        public string UserName { get; init; }
        public string Email { get; init; }
        public string? PhoneNumber { get; init; }
    }
}
