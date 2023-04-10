namespace WebApi.Core.Exceptions.Publisher
{
    public class UnprocessablePublisherException : UnprocessableEntityException
    {
        public UnprocessablePublisherException(string errors) : base($"Entity can't be processed! Error: {errors}")
        {
        }
    }
}
