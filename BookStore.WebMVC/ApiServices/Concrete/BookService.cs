using AuthorStore.WebMVC.ApiServices.Abstract;
using AutoMapper;
using BookStore.WebMVC.ApiServices.Abstract;
using BookStore.WebMVC.Models;
using PublisherStore.WebMVC.ApiServices.Abstract;
using WebApi.Core.Consts;
using WebApi.Core.Models.Book;
using WebApi.Core.RequestFilters;
using WebApi.Core.RequestFilters.Book;

namespace BookStore.ApiServices.Concrete
{
    public class BookService : IBookService
    {
        #region Fields
        private readonly IHttpClientService _httpClient;
        private readonly IAuthorService _authorService;
        private readonly IPublisherService _publisherService;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public BookService(IHttpClientService httpClient, IAuthorService authorService, IPublisherService publisherService, IMapper mapper)
        {
            _httpClient = httpClient;
            _authorService = authorService;
            _publisherService = publisherService;
            _mapper = mapper;
        }
        #endregion

        public async Task<(ResponseMessage<List<BookDto>> response, Metadata metadata)> GetBooksAsync(BookRequestFilter filter = null) =>
            await _httpClient.GetAllAsync<List<BookDto>>($"{WebApiConsts.Api}/{WebApiConsts.Books}", filter);

        public async Task<ResponseMessage<BookDto>> GetBookAsync(Guid id) =>
            await _httpClient.GetAsync<BookDto>($"{WebApiConsts.Api}/{WebApiConsts.Books}", id.ToString());

        public async Task<BookAddDto> AddBookIncludeRelations()
        {
            var bookDto = new BookAddDto();
            bookDto.Publishers = (await _publisherService.GetPublishersAsync()).response.Content;
            return bookDto;
        }

        public async Task<BookUpdateDto> UpdateBookIncludeRelations(Guid id)
        {
            var bookDto = (await GetBookAsync(id)).Content;
            var map = _mapper.Map<BookUpdateDto>(bookDto);
            map.Publishers = (await _publisherService.GetPublishersAsync()).response.Content;

            return map;
        }
        public async Task<ResponseMessage<BookAddDto>> AddBookAsync(BookAddDto bookAddDto) =>
            await _httpClient.PostInBodyAsync<BookAddDto>($"{WebApiConsts.Api}/{WebApiConsts.Books}/{WebApiConsts.Add}", bookAddDto);

        public async Task<ResponseMessage<BookUpdateDto>> UpdateBookAsync(BookUpdateDto bookUpdateDto) =>
            await _httpClient.PostInBodyAsync<BookUpdateDto>($"{WebApiConsts.Api}/{WebApiConsts.Books}/{WebApiConsts.Update}", bookUpdateDto);

        public async Task<ResponseMessage<string>> DeleteBookAsync(Guid id) =>
            await _httpClient.PostInRoute<string>($"{WebApiConsts.Api}/{WebApiConsts.Books}/{WebApiConsts.Delete}", id.ToString());

        public async Task<ResponseMessage<string>> SafeDeleteBookAsync(Guid id) =>
            await _httpClient.PostInRoute<string>($"{WebApiConsts.Api}/{WebApiConsts.Books}/{WebApiConsts.SafeDelete}", id.ToString());
    }
}
