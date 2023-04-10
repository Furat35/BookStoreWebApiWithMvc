namespace WebApi.Core.Exceptions.AppUser
{
    public class AppUserAlreadyExistsException : BadRequestException
    {
        public AppUserAlreadyExistsException(string userName) : base($"The user : {userName} already exists!")
        {

        }
    }
}
