namespace WebApi.Core.Exceptions.Publisher
{
    public class PublisherNotFoundException : NotFoundException
    {
        public PublisherNotFoundException(Guid id) : base($"The publisher with id : {id} could not be found.")
        {
        }
    }
}
