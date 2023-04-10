using AutoMapper;
using WebApi.Core.Models.Publisher;
using WebApi.Entity.Entities;

namespace WebApi.Service.Mappers
{
    public class PublisherMapper : Profile
    {
        public PublisherMapper()
        {
            CreateMap<Publisher, PublisherDto>();
            CreateMap<PublisherAddDto, Publisher>();
            CreateMap<PublisherUpdateDto, Publisher>();
        }
    }
}
