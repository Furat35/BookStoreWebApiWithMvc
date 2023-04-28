namespace WebApi.Core.Models.Genres
{
    public record GenreAddDto
    {
        public GenreAddDto()
        {

        }
        public GenreAddDto(string name)
        {
            Name = name;
        }
        public string Name { get; init; }
    }
}
