namespace WebApi.Core.Models.Publisher
{
    public record PublisherUpdateDto
    {
        public PublisherUpdateDto()
        {

        }
        public Guid Id { get; init; }
        public string PublisherName { get; init; }
    }
}
