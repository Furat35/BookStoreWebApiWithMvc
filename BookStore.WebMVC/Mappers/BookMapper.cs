using AutoMapper;
using WebApi.Core.Models.Book;

namespace BookStore.WebMVC.Mappers
{
    public class BookMapper : Profile
    {
        public BookMapper()
        {
            CreateMap<BookDto, BookUpdateDto>();
            CreateMap<BookAddDto, BookDto>();
            CreateMap<BookDto, BookAddDto>();
        }
    }
}
