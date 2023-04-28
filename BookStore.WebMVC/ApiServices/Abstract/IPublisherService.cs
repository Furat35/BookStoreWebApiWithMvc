using BookStore.WebMVC.Models;
using WebApi.Core.Models.Publisher;
using WebApi.Core.RequestFilters;
using WebApi.Core.RequestFilters.Publisher;

namespace PublisherStore.WebMVC.ApiServices.Abstract
{
    public interface IPublisherService
    {
        Task<(ResponseMessage<List<PublisherDto>> response, Metadata metadata)> GetPublishersAsync(PublisherRequestFilter filter = null);
        Task<ResponseMessage<PublisherDto>> GetPublisherAsync(Guid id);
        Task<ResponseMessage<PublisherAddDto>> AddPublisherAsync(PublisherAddDto PublisherAddDto);
        Task<ResponseMessage<PublisherUpdateDto>> UpdatePublisherAsync(PublisherUpdateDto PublisherUpdateDto);
        Task<ResponseMessage<string>> DeletePublisherAsync(Guid id);
        Task<ResponseMessage<string>> SafeDeletePublisherAsync(Guid id);
    }
}
