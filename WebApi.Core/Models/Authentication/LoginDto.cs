namespace WebApi.Core.Models.Authentication
{
    public record LoginDto
    {
        public LoginDto()
        {

        }
        public LoginDto(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
        public string UserName { get; init; }
        public string Password { get; init; }
    }
}
