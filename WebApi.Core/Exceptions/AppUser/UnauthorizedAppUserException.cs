namespace WebApi.Core.Exceptions.AppUser
{
    public class UnauthorizedAppUserException : UnauthorizedException
    {
        public UnauthorizedAppUserException(string message = "Unauthorized Access!") : base(message)
        {
        }
    }
}
