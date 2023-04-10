namespace WebApi.Core.Models.Author
{
    public record AuthorUpdateDto
    {
        public AuthorUpdateDto()
        {

        }
        public Guid Id { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Phone { get; init; }
        public string Mail { get; init; }
        public DateTime? BirthDate { get; init; }
    }
}
