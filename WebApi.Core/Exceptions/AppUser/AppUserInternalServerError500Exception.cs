namespace WebApi.Core.Exceptions.AppUser
{
    public class AppUserInternalServerError500Exception : InternalServerErrorException
    {
        public AppUserInternalServerError500Exception(string message = "A server side problem occured! Please try again.") : base(message)
        {
        }
    }
}
