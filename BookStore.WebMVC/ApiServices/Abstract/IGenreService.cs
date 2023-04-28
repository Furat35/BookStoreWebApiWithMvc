
using BookStore.WebMVC.Models;
using WebApi.Core.Models.Genres;
using WebApi.Core.RequestFilters;
using WebApi.Core.RequestFilters.Genre;

namespace GenreStore.WebMVC.ApiServices.Abstract
{
    public interface IGenreService
    {
        Task<(ResponseMessage<List<GenreDto>> response, Metadata metadata)> GetGenresAsync(GenreRequestFilter filter = null);
        Task<ResponseMessage<GenreDto>> GetGenreAsync(Guid id);
        Task<ResponseMessage<GenreAddDto>> AddGenreAsync(GenreAddDto GenreAddDto);
        Task<ResponseMessage<GenreUpdateDto>> UpdateGenreAsync(GenreUpdateDto GenreUpdateDto);
        Task<ResponseMessage<string>> DeleteGenreAsync(Guid id);
        Task<ResponseMessage<string>> SafeDeleteGenreAsync(Guid id);
    }
}
