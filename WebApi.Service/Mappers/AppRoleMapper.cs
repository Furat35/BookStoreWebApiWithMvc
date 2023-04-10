using AutoMapper;
using WebApi.Core.Models.AppRole;
using WebApi.Entity.Entities;

namespace WebApi.Service.Mappers
{
    public class AppRoleMapper : Profile
    {
        public AppRoleMapper()
        {
            CreateMap<AppRole, AppRoleDto>();
            CreateMap<AppRoleUpdateDto, AppRole>();
            CreateMap<AppRoleAddDto, AppRole>();
        }
    }
}
