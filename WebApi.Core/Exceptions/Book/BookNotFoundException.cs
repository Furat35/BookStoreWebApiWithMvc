namespace WebApi.Core.Exceptions.Book
{
    public class BookNotFoundException : NotFoundException
    {
        public BookNotFoundException(Guid id) : base($"The book with id : {id} could not be found.")
        {
        }
    }
}
