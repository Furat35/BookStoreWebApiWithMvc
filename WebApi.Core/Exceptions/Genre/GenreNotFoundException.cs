namespace WebApi.Core.Exceptions.Genre
{
    public class GenreNotFoundException : NotFoundException
    {
        public GenreNotFoundException(Guid id) : base($"The genre with id : {id} is not found.")
        {
        }
    }
}
