namespace WebApi.Core.Exceptions.AppUser
{
    public class AppUserNotFoundException : BadRequestException
    {
        public AppUserNotFoundException(string user) : base($"The user : {user} does not exist!")
        {

        }
    }
}
