namespace WebApi.Core.Exceptions.Author
{
    public class UnprocessableAuthorException : UnprocessableEntityException
    {
        public UnprocessableAuthorException(string errors = "Invalid transaction! Fiil the blanks and give valid inputs.") : base(errors)
        {
        }
    }
}
