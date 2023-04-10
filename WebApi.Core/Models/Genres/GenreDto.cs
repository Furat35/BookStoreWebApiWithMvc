namespace WebApi.Core.Models.Genres
{
    public record GenreDto
    {
        public GenreDto()
        {

        }
        public Guid Id { get; init; }
        public string Name { get; init; }
    }
}
