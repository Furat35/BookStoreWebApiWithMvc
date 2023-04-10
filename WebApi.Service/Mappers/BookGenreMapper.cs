using AutoMapper;
using WebApi.Core.Models.BookGenre;
using WebApi.Entity.Entities;

namespace WebApi.Service.Mappers
{
    public class BookGenreMapper : Profile
    {
        public BookGenreMapper()
        {
            CreateMap<BookGenre, BookGenreDto>();
            CreateMap<BookGenreAddDto, BookGenre>();
            CreateMap<BookGenreUpdateDto, BookGenre>();
        }
    }
}
