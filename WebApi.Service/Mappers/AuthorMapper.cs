using AutoMapper;
using WebApi.Core.Models.Author;
using WebApi.Entity.Entities;

namespace WebApi.Service.Mappers
{
    public class AuthorMapper : Profile
    {
        public AuthorMapper()
        {
            CreateMap<Author, AuthorDto>();
            CreateMap<AuthorAddDto, Author>();
            CreateMap<AuthorUpdateDto, Author>();
        }
    }
}
