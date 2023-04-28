using BookStore.WebMVC.Models;
using WebApi.Core.RequestFilters;

namespace BookStore.WebMVC.ApiServices.Abstract
{
    public interface IHttpClientService
    {
        Task<ResponseMessage<T>> GetAsync<T>(string url, string id = null) where T : class;
        Task<(ResponseMessage<T> response, Metadata metadata)> GetAllWithQueryStringAsync<T>(string url, object filter) where T : class;
        Task<(ResponseMessage<T> response, Metadata metadata)> GetAllAsync<T>(string url, object filter) where T : class;
        Task<ResponseMessage<T>> PostInBodyAsync<T>(string url, T entity) where T : class;
        Task<ResponseMessage<T>> PostInRoute<T>(string url, string routeValue) where T : class;
    }
}
