namespace WebApi.Core.Models.Publisher
{
    public record PublisherDto
    {
        public PublisherDto()
        {

        }
        public Guid Id { get; init; }
        public string PublisherName { get; init; }
    }
}
