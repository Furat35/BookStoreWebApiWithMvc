namespace WebApi.Core.Exceptions.Publisher
{
    public class PublisherInternalServerError500Exception : InternalServerErrorException
    {
        public PublisherInternalServerError500Exception(string message = "A server side error occured! Please try again.") : base(message)
        {
        }
    }
}
