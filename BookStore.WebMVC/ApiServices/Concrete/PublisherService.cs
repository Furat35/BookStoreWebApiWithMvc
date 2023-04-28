using BookStore.WebMVC.ApiServices.Abstract;
using BookStore.WebMVC.Models;
using PublisherStore.WebMVC.ApiServices.Abstract;
using WebApi.Core.Consts;
using WebApi.Core.Models.Publisher;
using WebApi.Core.RequestFilters;
using WebApi.Core.RequestFilters.Publisher;

namespace BookStore.ApiServices.Concrete
{
    public class PublisherService : IPublisherService
    {
        #region Fields
        private readonly IHttpClientService _httpClient;
        #endregion

        #region Ctor
        public PublisherService(IHttpClientService httpClient)
        {
            _httpClient = httpClient;
        }
        #endregion

        public async Task<(ResponseMessage<List<PublisherDto>> response, Metadata metadata)> GetPublishersAsync(PublisherRequestFilter filter = null) =>
            await _httpClient.GetAllAsync<List<PublisherDto>>($"{WebApiConsts.Api}/{WebApiConsts.Publishers}", filter);

        public async Task<ResponseMessage<PublisherDto>> GetPublisherAsync(Guid id) =>
            await _httpClient.GetAsync<PublisherDto>($"{WebApiConsts.Api}/{WebApiConsts.Publishers}", id.ToString());

        public async Task<ResponseMessage<PublisherAddDto>> AddPublisherAsync(PublisherAddDto PublisherAddDto) =>
            await _httpClient.PostInBodyAsync<PublisherAddDto>($"{WebApiConsts.Api}/{WebApiConsts.Publishers}/{WebApiConsts.Add}", PublisherAddDto);

        public async Task<ResponseMessage<PublisherUpdateDto>> UpdatePublisherAsync(PublisherUpdateDto PublisherUpdateDto) =>
            await _httpClient.PostInBodyAsync<PublisherUpdateDto>($"{WebApiConsts.Api}/{WebApiConsts.Publishers}/{WebApiConsts.Update}", PublisherUpdateDto);

        public async Task<ResponseMessage<string>> DeletePublisherAsync(Guid id) =>
            await _httpClient.PostInRoute<string>($"{WebApiConsts.Api}/{WebApiConsts.Publishers}/{WebApiConsts.Delete}", id.ToString());

        public async Task<ResponseMessage<string>> SafeDeletePublisherAsync(Guid id) =>
            await _httpClient.PostInRoute<string>($"{WebApiConsts.Api}/{WebApiConsts.Publishers}/{WebApiConsts.SafeDelete}", id.ToString());
    }
}
