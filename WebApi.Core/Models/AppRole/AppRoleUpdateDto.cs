namespace WebApi.Core.Models.AppRole
{
    public record AppRoleUpdateDto
    {
        public AppRoleUpdateDto()
        {

        }
        public AppRoleUpdateDto(string name)
        {
            Name = name;
        }

        public Guid Id { get; init; }
        public string Name { get; init; }
    }
}
