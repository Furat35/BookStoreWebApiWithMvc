using AutoMapper;
using WebApi.Core.Models.AppUser;
using WebApi.Entity.Entities;

namespace WebApi.Service.Mappers
{
    public class AppUserMapper : Profile
    {
        public AppUserMapper()
        {
            CreateMap<AppUser, AppUserDto>();
            CreateMap<AppUserUpdateDto, AppUser>();
            CreateMap<AppUserAddDto, AppUser>();
        }
    }
}
