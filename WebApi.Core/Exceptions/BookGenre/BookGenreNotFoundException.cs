namespace WebApi.Core.Exceptions.BookGenre
{
    public class BookGenreNotFoundException : NotFoundException
    {
        public BookGenreNotFoundException(Guid id) : base($"The book with id : {id} could not be found.")
        {
        }
    }
}
