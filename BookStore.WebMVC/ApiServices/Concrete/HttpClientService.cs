using BookStore.WebMVC.ApiServices.Abstract;
using BookStore.WebMVC.Consts;
using BookStore.WebMVC.Extensions;
using BookStore.WebMVC.Models;
using System.Text.Json;
using WebApi.Core.RequestFilters;

namespace BookStore.ApiServices.Concrete
{
    public class HttpClientService : IHttpClientService
    {
        #region Fields
        private readonly HttpClient _httpClient;
        #endregion

        #region Ctor
        public HttpClientService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient(HttpClientConsts.DefaultName);
        }
        #endregion

        public async Task<ResponseMessage<T>> GetAsync<T>(string url, string id = null) where T : class
        {
            var response = await _httpClient.GetAsync(string.Concat(url, "/", id));
            return await AnalyzeResponseAsync<T>(response);
        }

        public async Task<(ResponseMessage<T> response, Metadata metadata)> GetAllAsync<T>(string url, object filter) where T : class
        {
            var query = new QueryStringFormatter().FormatQuery(filter);
            var response = await _httpClient.GetAsync(string.Concat(url, "?", query));
            Metadata metadata = null;
            if (response.IsSuccessStatusCode)
            {
                var header = response.Headers.GetValues("X-Pagination").First();
                metadata = JsonSerializer.Deserialize<Metadata>(header);
            }

            return (await AnalyzeResponseAsync<T>(response), metadata);
        }


        public async Task<ResponseMessage<T>> PostInBodyAsync<T>(string url, T entity) where T : class
        {
            var response = await _httpClient.PostAsJsonAsync(url, entity);
            string readContent = await response.Content.ReadAsStringAsync();

            return await AnalyzeResponseAsync<T>(response);
        }

        public async Task<ResponseMessage<T>> PostInRoute<T>(string url, string routeValue) where T : class
        {
            var response = await _httpClient.PostAsync(string.Concat(url, "/", routeValue), null);
            return await AnalyzeResponseAsync<T>(response);
        }

        private async Task<ResponseMessage<T>> AnalyzeResponseAsync<T>(HttpResponseMessage response) where T : class
        {
            string readContent = await response.Content.ReadAsStringAsync();
            return response.IsSuccessStatusCode
               ? ResponseMessages.SuccessResponse<T>(readContent)
               : ResponseMessages.ErrorResponse<T>(readContent);
        }

        public Task<(ResponseMessage<T> response, Metadata metadata)> GetAllWithQueryStringAsync<T>(string url, object filter) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
