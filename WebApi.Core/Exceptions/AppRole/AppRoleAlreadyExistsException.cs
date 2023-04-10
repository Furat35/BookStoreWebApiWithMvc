namespace WebApi.Core.Exceptions.AppRole
{
    public class AppRoleAlreadyExistsException : BadRequestException
    {
        public AppRoleAlreadyExistsException(string roleName) : base($"The role : {roleName} already exists!")
        {
        }
    }
}
