using AutoMapper;
using WebApi.Core.Models.Book;
using WebApi.Entity.Entities;

namespace WebApi.Service.Mappers
{
    public class BookMapper : Profile
    {
        public BookMapper()
        {
            CreateMap<Book, BookDto>();
            CreateMap<BookAddDto, Book>();
            CreateMap<BookUpdateDto, Book>();
        }
    }
}
