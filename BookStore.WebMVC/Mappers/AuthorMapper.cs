using AutoMapper;
using WebApi.Core.Models.Author;

namespace BookStore.WebMVC.Mappers
{
    public class AuthorMapper : Profile
    {
        public AuthorMapper()
        {
            CreateMap<AuthorUpdateDto, AuthorDto>().ReverseMap();
        }
    }
}
