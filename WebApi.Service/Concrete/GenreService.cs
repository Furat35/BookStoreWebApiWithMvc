using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApi.Core.Exceptions.Genre;
using WebApi.Core.Models.Genres;
using WebApi.Core.RequestFilters;
using WebApi.Core.RequestFilters.Genre;
using WebApi.DataAccess.Abstract;
using WebApi.DataAccess.UnitOfWorks;
using WebApi.Entity.Entities;
using WebApi.Service.Abstract;
using WebApi.Service.Extensions.Filters;

namespace WebApi.Service.Concrete
{
    public sealed class GenreService : IGenreService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<Genre> _validator;
        private readonly IRepository<Genre> _genreRepo;
        #endregion

        #region Ctor
        public GenreService(IUnitOfWork unitOfWork, IMapper mapper, IRepository<Genre> genreRepo, IValidator<Genre> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
            _genreRepo = unitOfWork.GetRepository<Genre>();
        }
        #endregion

        public async Task<(List<GenreDto> genres, Metadata metadata)> GetGenresAsync(Expression<Func<Genre, bool>> predicate = null,
            GenreRequestFilter filters = null)
        {
            var genres = _genreRepo
                .GetAllAsync(predicate)
                .GetFilteredBooks(filters);

            var filteredGenres = await genres
                .Skip((filters.Page - 1) * filters.PageSize)
                .Take(filters.PageSize)
                .ToListAsync();

            Metadata metadata = new Metadata()
            {
                CurrentPage = filters.Page,
                PageSize = filters.PageSize,
                //TotalPages = genres.Count() / filters.PageSize,
                TotalEntities = genres.Count()
            };

            return filteredGenres is not null
                ? (_mapper.Map<List<GenreDto>>(filteredGenres), metadata)
                : (null, metadata);
        }

        public async Task<GenreDto> GetGenreByGuidAsync(Guid id)
        {
            var genre = await GenreExistsAsync(id);
            return _mapper.Map<GenreDto>(genre);
        }

        public async Task<GenreDto> AddGenreAsync(GenreAddDto entity)
        {
            var genre = _mapper.Map<Genre>(entity);
            await GenreValidatorAsync(genre);
            _genreRepo.Add(genre);
            await SaveGenreAsync();

            return _mapper.Map<GenreDto>(genre);
        }

        public async Task SafeDeleteGenreAsync(Guid id)
        {
            var genre = await GenreExistsAsync(id);
            if (!genre.IsDeleted)
            {
                genre.IsDeleted = true;
                _genreRepo.SafeDelete(genre);
                await SaveGenreAsync();
            }
        }

        public async Task UpdateGenreAsync(GenreUpdateDto entity)
        {
            var genre = await GenreExistsAsync(entity.Id);
            var map = _mapper.Map<Genre>(entity);
            await GenreValidatorAsync(map);
            _mapper.Map(entity, genre);
            await SaveGenreAsync();
        }

        #region Private Methods
        private async Task SaveGenreAsync()
        {
            int effectedRows;
            try
            {
                effectedRows = await _unitOfWork.SaveAsync();
            }
            catch (Exception e)
            {
                throw new GenreInternalServerError500Exception(e.Message);
            }

            if (effectedRows != 1)
                throw new GenreInternalServerError500Exception();
        }

        private async Task GenreValidatorAsync(Genre genre)
        {
            var result = await _validator.ValidateAsync(genre);
            if (!result.IsValid)
                throw new UnprocessableGenreException();
        }

        private async Task<Genre> GenreExistsAsync(Guid id)
        {
            var genre = await _genreRepo.GetByGuidAsync(id);
            if (genre is null)
                throw new GenreNotFoundException(id);

            return genre;
        }
        #endregion

        #region Functions that can be needed later
        //public async Task<GenreDto> GetFirstGenreAsync(Expression<Func<Genre, bool>> predicate, bool trackChanges = false)
        //{
        //    var genre = await _genreRepo.GetFirstAsync(predicate, trackChanges);
        //    return genre is not null
        //    ? _mapper.Map<GenreDto>(genre)
        //    : null;
        //}

        //public async Task DeleteGenreAsync(Guid id)
        //{
        //    var genre = await GenreExistsAsync(id);
        //    _genreRepo.Delete(genre);
        //    await SaveGenreAsync();
        //}

        //public async Task<int> CountGenresAsync(Expression<Func<Genre, bool>> predicate)
        //{
        //    return await _genreRepo.CountAsync(predicate);
        //}
        #endregion
    }
}
