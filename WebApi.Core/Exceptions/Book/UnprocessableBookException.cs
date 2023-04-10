namespace WebApi.Core.Exceptions.Book
{
    public class UnprocessableBookException : UnprocessableEntityException
    {
        public UnprocessableBookException(string errors) : base($"Entity can't be processed! Error: {errors}")
        {
        }
    }
}
