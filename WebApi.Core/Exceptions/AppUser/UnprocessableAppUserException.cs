namespace WebApi.Core.Exceptions.AppUser
{
    public class UnprocessableAppUserException : UnprocessableEntityException
    {
        public UnprocessableAppUserException(string errors = "Invalid transaction! Fiil the blanks and give valid inputs.") : base(errors)
        {
        }
    }
}
