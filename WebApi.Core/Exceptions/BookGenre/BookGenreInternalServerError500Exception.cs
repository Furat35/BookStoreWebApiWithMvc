namespace WebApi.Core.Exceptions.BookGenre
{
    public class BookGenreInternalServerError500Exception : InternalServerErrorException
    {
        public BookGenreInternalServerError500Exception(string message = "A server side error occured! Please try again.") : base(message)
        {
        }
    }
}
