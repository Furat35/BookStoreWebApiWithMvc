using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace WebApi.Core.Models.Publisher
{
    public record PublisherAddDto
    {
        public PublisherAddDto()
        {

        }
        public PublisherAddDto(string publisherName)
        {
            
            PublisherName = publisherName;
        }
        public string PublisherName { get; init; }
    }
}
