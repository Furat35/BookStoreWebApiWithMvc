namespace WebApi.Core.Exceptions.AppRole
{
    public class AppRoleNotFoundException : BadRequestException
    {
        public AppRoleNotFoundException(string role) : base($"The role : {role} does not exist")
        {
        }
    }
}
