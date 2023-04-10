namespace WebApi.Core.Models.Genres
{
    public record GenreUpdateDto
    {
        public GenreUpdateDto()
        {

        }
        public Guid Id { get; init; }
        public string Name { get; init; }
    }
}
