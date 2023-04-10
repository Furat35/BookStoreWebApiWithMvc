namespace WebApi.Core.Exceptions.Author
{
    public class AuthorNotFoundException : NotFoundException
    {
        public AuthorNotFoundException(Guid id) : base($"The author with id : {id} could not be found.")
        {
        }
    }
}
