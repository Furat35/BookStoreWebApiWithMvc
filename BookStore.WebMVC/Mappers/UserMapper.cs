using AutoMapper;
using WebApi.Core.Models.AppUser;

namespace BookStore.WebMVC.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<AppUserDto, AppUserUpdateDto>().ReverseMap();
        }
    }
}
