using AutoMapper;
using WebApi.Core.Models.Publisher;

namespace BookStore.WebMVC.Mappers
{
    public class PublisherMapper : Profile
    {
        public PublisherMapper()
        {
            CreateMap<PublisherDto, PublisherUpdateDto>().ReverseMap();
        }
    }
}
