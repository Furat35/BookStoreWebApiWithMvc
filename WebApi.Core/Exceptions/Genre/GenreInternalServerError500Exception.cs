namespace WebApi.Core.Exceptions.Genre
{
    public class GenreInternalServerError500Exception : InternalServerErrorException
    {
        public GenreInternalServerError500Exception(string message = "A server side error occured! Please try again.") : base(message)
        {
        }
    }
}
