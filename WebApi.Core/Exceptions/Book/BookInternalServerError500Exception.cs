namespace WebApi.Core.Exceptions.Book
{
    public class BookInternalServerError500Exception : InternalServerErrorException
    {
        public BookInternalServerError500Exception(string message = "A server side error occured! Please try again.") : base(message)
        {
        }
    }
}
