using BookStore.WebMVC.Models;
using WebApi.Core.Models.Book;
using WebApi.Core.RequestFilters;
using WebApi.Core.RequestFilters.Book;

namespace BookStore.WebMVC.ApiServices.Abstract
{
    public interface IBookService
    {

        Task<(ResponseMessage<List<BookDto>> response, Metadata metadata)> GetBooksAsync(
            BookRequestFilter filters = null);
        Task<ResponseMessage<BookDto>> GetBookAsync(Guid id);
        Task<ResponseMessage<BookAddDto>> AddBookAsync(BookAddDto bookAddDto);
        Task<ResponseMessage<BookUpdateDto>> UpdateBookAsync(BookUpdateDto bookUpdateDto);
        Task<ResponseMessage<string>> DeleteBookAsync(Guid id);
        Task<ResponseMessage<string>> SafeDeleteBookAsync(Guid id);
        Task<BookAddDto> AddBookIncludeRelations();
        Task<BookUpdateDto> UpdateBookIncludeRelations(Guid id);
    }
}
