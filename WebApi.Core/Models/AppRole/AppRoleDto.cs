namespace WebApi.Core.Models.AppRole
{
    public record AppRoleDto
    {
        public AppRoleDto()
        {

        }

        public AppRoleDto(string name)
        {
            Name = name;
        }
        public Guid Id { get; init; }
        public string Name { get; init; }
    }
}
