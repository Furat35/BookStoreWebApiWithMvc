namespace WebApi.Core.Models.AppRole
{
    public record AppRoleAddDto
    {
        public AppRoleAddDto()
        {

        }
        public AppRoleAddDto(string name)
        {
            Name = name;
        }
        public string Name { get; init; }
    }
}
