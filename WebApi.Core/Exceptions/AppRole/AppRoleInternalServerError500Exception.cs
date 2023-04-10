namespace WebApi.Core.Exceptions.AppRole
{
    public class AppRoleInternalServerError500Exception : InternalServerErrorException
    {
        public AppRoleInternalServerError500Exception(string message = "A server side problem occured! Please try again.") : base(message)
        {
        }
    }
}
