namespace WebApi.Core.Models
{
    public class LoginResponse
    {
        public IList<string> Roles { get; set; }
        public JwtResponse JwtResponse { get; set; }
    }
}
