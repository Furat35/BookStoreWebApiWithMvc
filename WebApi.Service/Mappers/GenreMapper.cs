using AutoMapper;
using WebApi.Core.Models.Genres;
using WebApi.Entity.Entities;

namespace WebApi.Service.Mappers
{
    public class GenreMapper : Profile
    {
        public GenreMapper()
        {
            CreateMap<Genre, GenreDto>();
            CreateMap<GenreAddDto, Genre>();
            CreateMap<GenreUpdateDto, Genre>();
        }
    }
}
