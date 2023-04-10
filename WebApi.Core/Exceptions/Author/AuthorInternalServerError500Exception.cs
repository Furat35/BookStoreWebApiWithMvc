namespace WebApi.Core.Exceptions.Author
{
    public class AuthorInternalServerError500Exception : InternalServerErrorException
    {
        public AuthorInternalServerError500Exception(string message = "A server side problem occured! Please try again.") : base(message)
        {
        }
    }
}
