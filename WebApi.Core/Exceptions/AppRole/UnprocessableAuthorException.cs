namespace WebApi.Core.Exceptions.AppRole
{
    public class UnprocessableAppRoleException : UnprocessableEntityException
    {
        public UnprocessableAppRoleException(string errors = "Invalid transaction! Fiil the blanks and give valid inputs.") : base(errors)
        {
        }
    }
}
