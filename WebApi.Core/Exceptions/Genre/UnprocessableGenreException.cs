namespace WebApi.Core.Exceptions.Genre
{
    public class UnprocessableGenreException : UnprocessableEntityException
    {
        public UnprocessableGenreException(string errors = "Invalid transaction! Fiil the blanks and give valid inputs.") : base(errors)
        {
        }
    }
}
