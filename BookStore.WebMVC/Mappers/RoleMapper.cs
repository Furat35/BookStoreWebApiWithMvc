using AutoMapper;
using WebApi.Core.Models.AppRole;

namespace BookStore.WebMVC.Mappers
{
    public class RoleMapper : Profile
    {
        public RoleMapper()
        {
            CreateMap<AppRoleDto, AppRoleUpdateDto>().ReverseMap();
        }
    }
}
