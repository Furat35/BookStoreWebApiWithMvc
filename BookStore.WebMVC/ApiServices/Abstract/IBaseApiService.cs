using BookStore.WebMVC.Models;

namespace BookStore.WebMVC.ApiServices.Abstract
{
    public interface IBaseApiService<T>
    {
        Task<ResponseMessage<List<T>>> GetBooksAsync();
        Task<ResponseMessage<T>> GetBookAsync(Guid id);
        Task<ResponseMessage<T>> AddBookAsync(T entityAddDto);
        Task<ResponseMessage<T>> UpdateBookAsync(T entityUpdateDto);
        Task<ResponseMessage<T>> DeleteBookAsync(Guid id);
        Task<ResponseMessage<T>> SafeDeleteBookAsync(Guid id);
    }
}
