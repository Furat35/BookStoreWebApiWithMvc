using AutoMapper;
using WebApi.Core.Models.Genres;

namespace BookStore.WebMVC.Mappers
{
    public class GenreMapper : Profile
    {
        public GenreMapper()
        {
            CreateMap<GenreDto, GenreUpdateDto>().ReverseMap();
        }
    }
}
