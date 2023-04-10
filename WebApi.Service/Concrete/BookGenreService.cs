using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApi.Core.Exceptions.BookGenre;
using WebApi.Core.Models.BookGenre;
using WebApi.Core.RequestFilters;
using WebApi.Core.RequestFilters.BookGenre;
using WebApi.DataAccess.Abstract;
using WebApi.DataAccess.UnitOfWorks;
using WebApi.Entity.Entities;
using WebApi.Service.Abstract;
using WebApi.Service.Extensions.Filters;

namespace WebApi.Service.Concrete
{
    public sealed class BookGenreService : IBookGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRepository<BookGenre> _bookGenreRepo;

        public BookGenreService(IUnitOfWork unitOfWork, IMapper mapper, IRepository<BookGenre> bookGenreRepo)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _bookGenreRepo = _unitOfWork.GetRepository<BookGenre>();
        }


        private async Task SaveBookGenreAsync()
        {
            int effectedRows;
            try
            {
                effectedRows = await _unitOfWork.SaveAsync();
            }
            catch (Exception e)
            {
                throw new BookGenreInternalServerError500Exception(e.Message);
            }

            if (effectedRows != 1)
                throw new BookGenreInternalServerError500Exception();
        }

        public async Task<(List<BookGenreDto>, Metadata metadata)> GetBookGenresAsync(Expression<Func<BookGenre, bool>> predicate = null, BookGenreRequestFilter filters = null, bool trackChanges = false)
        {
            var bookGenres = _bookGenreRepo.GetAllAsync(predicate, trackChanges);
            var filteredBookGenres = filters is not null
                ? await bookGenres
                    .GetFilteredBookGenres(filters)
                    .Skip((filters.Page - 1) * filters.PageSize)
                    .Take(filters.PageSize).ToListAsync()
                : await bookGenres.ToListAsync();

            Metadata metadata = new Metadata()
            {
                CurrentPage = filters.Page,
                PageSize = filters.PageSize,
                TotalPages = bookGenres.Count() / filters.PageSize,
                TotalEntities = bookGenres.Count()
            };

            return filteredBookGenres is not null
                ? (_mapper.Map<List<BookGenreDto>>(filteredBookGenres), metadata)
                : (null, metadata);
        }

        public async Task<BookGenreDto> GetBookGenreByGuidAsync(Guid id)
        {
            var bookGenres = await _bookGenreRepo.GetByGuidAsync(id);
            return bookGenres is not null ? _mapper.Map<BookGenreDto>(bookGenres) : null;
        }

        public async Task<BookGenreDto> GetFirstBookGenreAsync(Expression<Func<BookGenre, bool>> predicate, bool trackChanges = false)
        {
            var bookGenre = await _bookGenreRepo.GetFirstAsync(predicate, trackChanges);
            return bookGenre is not null ? _mapper.Map<BookGenreDto>(bookGenre) : null;
        }

        public async Task<BookGenreDto> AddBookGenreAsync(BookGenreAddDto entity)
        {
            var bookGenre = _mapper.Map<BookGenre>(entity);
            _bookGenreRepo.Add(bookGenre);
            await SaveBookGenreAsync();

            return _mapper.Map<BookGenreDto>(bookGenre);
        }

        public async Task DeleteBookGenreAsync(Guid id)
        {
            var bookGenre = await _bookGenreRepo.GetByGuidAsync(id);
            if (bookGenre is null)
                throw new BookGenreNotFoundException(id);

            _bookGenreRepo.Delete(bookGenre);
            await SaveBookGenreAsync();
        }

        public async Task SafeDeleteBookGenreAsync(Guid id)
        {
            var bookGenre = await _bookGenreRepo.GetByGuidAsync(id);
            if (bookGenre is null)
                throw new BookGenreNotFoundException(id);

            bookGenre.IsDeleted = true;
            _bookGenreRepo.SafeDelete(bookGenre);
            await SaveBookGenreAsync();
        }

        public async Task UpdateBookGenreAsync(BookGenreUpdateDto entity)
        {
            var bookGenre = await _bookGenreRepo.GetByGuidAsync(entity.Id);
            if (bookGenre is null)
                throw new BookGenreNotFoundException(entity.Id);

            _mapper.Map(entity, bookGenre);
            _bookGenreRepo.Update(bookGenre);
            await SaveBookGenreAsync();
        }

        public async Task<int> CountBookGenresAsync(Expression<Func<BookGenre, bool>> predicate)
        {
            return await _bookGenreRepo.CountAsync(predicate);
        }

        //private async Task<Book> BookExists(Guid id)
        //{
        //    //var book = _bookGenreRepo.
        //}
    }
}
