using BookStore.WebMVC.ApiServices.Abstract;
using BookStore.WebMVC.Models;
using GenreStore.WebMVC.ApiServices.Abstract;
using WebApi.Core.Consts;
using WebApi.Core.Models.Genres;
using WebApi.Core.RequestFilters;
using WebApi.Core.RequestFilters.Genre;

namespace BookStore.ApiServices.Concrete
{
    public class GenreService : IGenreService
    {
        #region Fields
        private readonly IHttpClientService _httpClient;
        #endregion

        #region Ctor
        public GenreService(IHttpClientService httpClient)
        {
            _httpClient = httpClient;
        }
        #endregion

        public async Task<(ResponseMessage<List<GenreDto>> response, Metadata metadata)> GetGenresAsync(GenreRequestFilter filter = null) =>
            await _httpClient.GetAllAsync<List<GenreDto>>($"{WebApiConsts.Api}/{WebApiConsts.Genres}", filter);

        public async Task<ResponseMessage<GenreDto>> GetGenreAsync(Guid id) =>
            await _httpClient.GetAsync<GenreDto>($"{WebApiConsts.Api}/{WebApiConsts.Genres}", id.ToString());

        public async Task<ResponseMessage<GenreAddDto>> AddGenreAsync(GenreAddDto GenreAddDto) =>
            await _httpClient.PostInBodyAsync<GenreAddDto>($"{WebApiConsts.Api}/{WebApiConsts.Genres}/{WebApiConsts.Add}", GenreAddDto);

        public async Task<ResponseMessage<GenreUpdateDto>> UpdateGenreAsync(GenreUpdateDto GenreUpdateDto) =>
            await _httpClient.PostInBodyAsync<GenreUpdateDto>($"{WebApiConsts.Api}/{WebApiConsts.Genres}/{WebApiConsts.Update}", GenreUpdateDto);

        public async Task<ResponseMessage<string>> DeleteGenreAsync(Guid id) =>
            await _httpClient.PostInRoute<string>($"{WebApiConsts.Api}/{WebApiConsts.Genres}/{WebApiConsts.Delete}", id.ToString());
        
        public async Task<ResponseMessage<string>> SafeDeleteGenreAsync(Guid id) =>
            await _httpClient.PostInRoute<string>($"{WebApiConsts.Api}/{WebApiConsts.Genres}/{WebApiConsts.SafeDelete}", id.ToString());
    }
}
