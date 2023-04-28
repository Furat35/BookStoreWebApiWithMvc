namespace WebApi.Core.Models.Genres
{
    public record GenreDto
    {
        public GenreDto()
        {

        }
        public GenreDto(string name)
        {
            Name = name;
        }
        public Guid Id { get; init; }
        public string Name { get; init; }
    }
}
