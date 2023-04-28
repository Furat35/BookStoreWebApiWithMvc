using WebApi.Core.Models.Author;
using WebApi.Core.Models.Genres;
using WebApi.Core.Models.Publisher;

namespace WebApi.Core.Models.Book
{
    public record BookAddDto
    {
        public BookAddDto()
        {

        }

        public string Name { get; init; }
        public string Summary { get; init; }
        public short Page { get; init; }
        public Guid AuthorId { get; set; }
        public List<AuthorDto>? Authors { get; set; } = new List<AuthorDto>();
        public Guid PublisherId { get; set; }
        public List<PublisherDto>? Publishers { get; set; } = new List<PublisherDto>();
        public Guid GenreId { get; set; }
        public List<GenreDto>? Genres { get; set; } = new List<GenreDto>();
    }
}
