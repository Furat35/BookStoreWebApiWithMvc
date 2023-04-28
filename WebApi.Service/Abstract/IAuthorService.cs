using System.Linq.Expressions;
using WebApi.Core.Models.Author;
using WebApi.Core.RequestFilters;
using WebApi.Core.RequestFilters.Auhtor;
using WebApi.Entity.Entities;

namespace WebApi.Service.Abstract
{
    public interface IAuthorService
    {
        Task<(List<AuthorDto> authors, Metadata metadata)> GetAuthorsAsync(Expression<Func<Author, bool>> predicate = null, AuthorRequestFilter filters = null);
        Task<AuthorDto> GetAuthorByGuidAsync(Guid id);
        Task<AuthorDto> AddAuthorAsync(AuthorAddDto entity);
        Task SafeDeleteAuthorAsync(Guid id);
        Task UpdateAuthorAsync(AuthorUpdateDto entity);
        //Task<AuthorDto> GetFirstAuthorAsync(Expression<Func<Author, bool>> predicate, bool trackChanges = false);
        //Task DeleteAuthorAsync(Guid id);
        //Task<int> CountAuthorsAsync(Expression<Func<Author, bool>> predicate);
    }
}
