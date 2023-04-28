namespace WebApi.Core.Models.Authentication
{
    public record RegisterDto
    {
        public RegisterDto()
        {

        }
        public RegisterDto(string userName, string password, string email)
        {
            UserName = userName;
            Email = email;
            Password = password;
        }

        public string UserName { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
    }
}
