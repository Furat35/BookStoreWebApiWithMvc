using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApi.Core.Exceptions;
using WebApi.Core.Exceptions.Author;
using WebApi.Core.Models.Author;
using WebApi.Core.RequestFilters;
using WebApi.Core.RequestFilters.Auhtor;
using WebApi.DataAccess.Abstract;
using WebApi.DataAccess.UnitOfWorks;
using WebApi.Entity.Entities;
using WebApi.Service.Abstract;
using static WebApi.Service.Extensions.Filters.AuthorFilters;

namespace WebApi.Service.Concrete
{
    public sealed class AuthorService : IAuthorService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<Author> _validator;
        private readonly IRepository<Author> _authorRepo;
        #endregion

        #region Ctor
        public AuthorService(IUnitOfWork unitOfWork, IMapper mapper, IRepository<Author> authorRepo, IValidator<Author> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
            _authorRepo = _unitOfWork.GetRepository<Author>();
        }
        #endregion

        public async Task<(List<AuthorDto> authors, Metadata metadata)> GetAuthorsAsync(
            Expression<Func<Author, bool>> predicate = null, AuthorRequestFilter filters = null)
        {
            var authors = _authorRepo
                .GetAllAsync(predicate)
                .GetFilterAuthors(filters);

            var authorSkip = (filters.Page - 1) * filters.PageSize;
            bool isValidPage = authors.Count() > authorSkip
                ? true
                : false;

            if (!isValidPage)
                throw new InvalidPageException();

            var filteredAuthors = await authors
                .Skip(authorSkip)
                .Take(filters.PageSize)
                .ToListAsync();

            Metadata metadata = new Metadata()
            {
                PageSize = filters.PageSize,
                CurrentPage = filters.Page,
                TotalPages = authors.Count() / filters.PageSize,
                TotalEntities = authors.Count()
            };

            return filteredAuthors is not null
                ? (_mapper.Map<List<AuthorDto>>(filteredAuthors), metadata)
                : (null, metadata);
        }

        public async Task<AuthorDto> GetAuthorByGuidAsync(Guid id)
        {
            var author = await AuthorExistsAsync(id);
            return _mapper.Map<AuthorDto>(author);
        }

        public async Task<AuthorDto> AddAuthorAsync(AuthorAddDto entity)
        {
            var author = _mapper.Map<Author>(entity);
            await AuthorValidatorAsync(author);
            _authorRepo.Add(author);
            await SaveAuthorAsync();

            return _mapper.Map<AuthorDto>(author);
        }

        public async Task SafeDeleteAuthorAsync(Guid id)
        {
            var author = await AuthorExistsAsync(id);
            if (!author.IsDeleted)
            {
                author.IsDeleted = true;
                _authorRepo.SafeDelete(author);
                await SaveAuthorAsync();
            }
        }

        public async Task UpdateAuthorAsync(AuthorUpdateDto entity)
        {
            var map = _mapper.Map<Author>(entity);
            await AuthorValidatorAsync(map);

            var author = await AuthorExistsAsync(id: entity.Id);
            _mapper.Map(entity, author);
            _authorRepo.Update(author);
            await SaveAuthorAsync();
        }

        #region Private Methods
        private async Task SaveAuthorAsync()
        {
            int effectedRows;
            try
            {
                effectedRows = await _unitOfWork.SaveAsync();
            }
            catch (Exception e)
            {
                throw new AuthorInternalServerError500Exception(e.Message);
            }

            if (effectedRows != 1)
                throw new AuthorInternalServerError500Exception();
        }

        private async Task AuthorValidatorAsync(Author author)
        {
            var result = await _validator.ValidateAsync(author);
            if (!result.IsValid)
                throw new UnprocessableAuthorException();
        }

        private async Task<Author> AuthorExistsAsync(Guid id)
        {
            var author = await _authorRepo.GetByGuidAsync(id);
            if (author is null)
                throw new AuthorNotFoundException(id);

            return author;
        }
        #endregion

        #region Functions that can be needed later
        //public async Task DeleteAuthorAsync(Guid id)
        //{
        //    var author = await AuthorExistsAsync(id: id);
        //    author.IsDeleted = true;
        //    _authorRepo.Update(author);
        //    await SaveAuthorAsync();
        //}

        //public async Task<AuthorDto> GetFirstAuthorAsync(Expression<Func<Author, bool>> predicate, bool trackChanges = false)
        //{
        //    var author = await _authorRepo
        //        .GetFirstAsync(predicate, trackChanges);
        //    return author is not null
        //        ? _mapper.Map<AuthorDto>(author)
        //        : null;
        //}

        //public async Task<int> CountAuthorsAsync(Expression<Func<Author, bool>> predicate)
        //{
        //    return await _authorRepo
        //        .CountAsync(predicate);
        //}
        #endregion
    }
}
