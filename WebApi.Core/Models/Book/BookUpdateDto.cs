using WebApi.Core.Models.Author;
using WebApi.Core.Models.Publisher;

namespace WebApi.Core.Models.Book
{
    public record BookUpdateDto
    {
        public BookUpdateDto()
        {

        }
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Summary { get; init; }
        public short Page { get; init; }
        public Guid AuthorId { get; init; }
        public ICollection<AuthorDto> Authors { get; set; }
        public Guid PublisherId { get; init; }
        public ICollection<PublisherDto> Publishers { get; set; }
    }
}
