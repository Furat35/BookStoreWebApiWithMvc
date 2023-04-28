using AuthorStore.WebMVC.ApiServices.Abstract;
using BookStore.WebMVC.ApiServices.Abstract;
using BookStore.WebMVC.Models;
using WebApi.Core.Consts;
using WebApi.Core.Models.Author;
using WebApi.Core.RequestFilters;
using WebApi.Core.RequestFilters.Auhtor;

namespace BookStore.ApiServices.Concrete
{
    public class AuthorService : IAuthorService
    {
        #region Fields
        private readonly IHttpClientService _httpClient;
        #endregion

        #region Ctor
        public AuthorService(IHttpClientService httpClient)
        {
            _httpClient = httpClient;
        }
        #endregion

        public async Task<(ResponseMessage<List<AuthorDto>> response, Metadata metadata)> GetAuthorsAsync(AuthorRequestFilter filter) =>
            await _httpClient.GetAllAsync<List<AuthorDto>>($"{WebApiConsts.Api}/{WebApiConsts.Authors}", filter);

        public async Task<ResponseMessage<AuthorDto>> GetAuthorAsync(Guid id) =>
            await _httpClient.GetAsync<AuthorDto>($"{WebApiConsts.Api}/{WebApiConsts.Authors}", id.ToString());


        public async Task<ResponseMessage<AuthorAddDto>> AddAuthorAsync(AuthorAddDto AuthorAddDto) =>
            await _httpClient.PostInBodyAsync<AuthorAddDto>($"{WebApiConsts.Api}/{WebApiConsts.Authors}/{WebApiConsts.Add}", AuthorAddDto);

        public async Task<ResponseMessage<AuthorUpdateDto>> UpdateAuthorAsync(AuthorUpdateDto AuthorUpdateDto) =>
            await _httpClient.PostInBodyAsync<AuthorUpdateDto>($"{WebApiConsts.Api}/{WebApiConsts.Authors}/{WebApiConsts.Update}", AuthorUpdateDto);

        public async Task<ResponseMessage<string>> DeleteAuthorAsync(Guid id) =>
            await _httpClient.PostInRoute<string>($"{WebApiConsts.Api}/{WebApiConsts.Authors}/{WebApiConsts.Delete}", id.ToString());

        public async Task<ResponseMessage<string>> SafeDeleteAuthorAsync(Guid id) =>
            await _httpClient.PostInRoute<string>($"{WebApiConsts.Api}/{WebApiConsts.Authors}/{WebApiConsts.SafeDelete}", id.ToString());
    }
}
