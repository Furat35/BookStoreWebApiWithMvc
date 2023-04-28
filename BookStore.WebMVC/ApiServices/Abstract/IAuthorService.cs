using BookStore.WebMVC.Models;
using WebApi.Core.Models.Author;
using WebApi.Core.RequestFilters;
using WebApi.Core.RequestFilters.Auhtor;

namespace AuthorStore.WebMVC.ApiServices.Abstract
{
    public interface IAuthorService
    {
        Task<(ResponseMessage<List<AuthorDto>> response, Metadata metadata)> GetAuthorsAsync(AuthorRequestFilter filter);
        Task<ResponseMessage<AuthorDto>> GetAuthorAsync(Guid id);
        Task<ResponseMessage<AuthorAddDto>> AddAuthorAsync(AuthorAddDto AuthorAddDto);
        Task<ResponseMessage<AuthorUpdateDto>> UpdateAuthorAsync(AuthorUpdateDto AuthorUpdateDto);
        Task<ResponseMessage<string>> DeleteAuthorAsync(Guid id);
        Task<ResponseMessage<string>> SafeDeleteAuthorAsync(Guid id);
    }
}
