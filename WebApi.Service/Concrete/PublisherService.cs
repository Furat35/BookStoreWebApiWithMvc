using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApi.Core.Exceptions;
using WebApi.Core.Exceptions.Book;
using WebApi.Core.Exceptions.Publisher;
using WebApi.Core.Models.Publisher;
using WebApi.Core.RequestFilters;
using WebApi.Core.RequestFilters.Publisher;
using WebApi.DataAccess.Abstract;
using WebApi.DataAccess.UnitOfWorks;
using WebApi.Entity.Entities;
using WebApi.Service.Abstract;
using WebApi.Service.Extensions.Filters;

namespace WebApi.Service.Concrete
{
    public sealed class PublisherService : IPublisherService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRepository<Publisher> _publisherRepo;
        private readonly IValidator<Publisher> _validator;
        #endregion

        #region Ctor
        public PublisherService(IUnitOfWork unitOfWork, IMapper mapper, IRepository<Publisher> publisherRepo, IValidator<Publisher> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _publisherRepo = publisherRepo;
            _validator = validator;
        }
        #endregion

        public async Task<(List<PublisherDto> publishers, Metadata metadata)> GetPublishersAsync(Expression<Func<Publisher, bool>> predicate = null, PublisherRequestFilter filters = null)
        {
            var publishers = _publisherRepo
                .GetAllAsync(predicate)
                .GetFilteredPublishers(filters);

            var publisherSkip = (filters.Page - 1) * filters.PageSize;
            bool isValidPage = publishers.Count() > publisherSkip
                ? true
                : false;

            if (!isValidPage)
                throw new InvalidPageException();

            var filteredPublishers = await publishers
                    .Skip(publisherSkip)
                    .Take(filters.PageSize)
                    .ToListAsync();

            Metadata metadata = new Metadata()
            {
                CurrentPage = filters.Page,
                PageSize = filters.PageSize,
                //TotalPages = publishers.Count() / filters.PageSize,
                TotalEntities = publishers.Count()
            };

            return filteredPublishers is not null
                ? (_mapper.Map<List<PublisherDto>>(filteredPublishers), metadata)
                : (null, metadata);
        }

        public async Task<PublisherDto> GetPublisherByGuidAsync(Guid id)
        {
            var publisher = await PublisherExistsAsync(id);
            return _mapper.Map<PublisherDto>(publisher);
        }

        public async Task<PublisherDto> AddPublisherAsync(PublisherAddDto entity)
        {
            var publisher = _mapper.Map<Publisher>(entity);
            await PublisherValidatorAsync(publisher);
            _publisherRepo.Add(publisher);
            await SavePublisherAsync();

            return _mapper.Map<PublisherDto>(publisher);
        }

        public async Task SafeDeletePublisherAsync(Guid id)
        {
            var publisher = await PublisherExistsAsync(id);
            publisher.IsDeleted = true;
            _publisherRepo.SafeDelete(publisher);
            await SavePublisherAsync();
        }

        public async Task UpdatePublisherAsync(PublisherUpdateDto entity)
        {
            var map = _mapper.Map<Publisher>(entity);
            await PublisherValidatorAsync(map);
            var publisher = await PublisherExistsAsync(entity.Id);
            _mapper.Map(entity, publisher);
            _publisherRepo.Update(publisher);
            await SavePublisherAsync();
        }

        #region Private Methods
        private async Task SavePublisherAsync()
        {
            int effectedRows;
            try
            {
                effectedRows = await _unitOfWork.SaveAsync();
            }
            catch (Exception e)
            {
                throw new PublisherInternalServerError500Exception(e.Message);
            }

            if (effectedRows != 1)
                throw new PublisherInternalServerError500Exception();
        }

        private async Task PublisherValidatorAsync(Publisher publisher)
        {
            var result = await _validator.ValidateAsync(publisher);
            if (!result.IsValid)
                throw new UnprocessableBookException(string.Join(", ", result.Errors.ConvertAll(_ => _.ToString())));
        }

        private async Task<Publisher> PublisherExistsAsync(Guid id)
        {
            var publisher = await _publisherRepo.GetByGuidAsync(id);
            if (publisher is null)
                throw new PublisherNotFoundException(id);

            return publisher;
        }
        #endregion

        #region Methods that can be needed later
        //public async Task<PublisherDto> GetFirstPublisherAsync(Expression<Func<Publisher, bool>> predicate, bool trackChanges = false)
        //{
        //    var publisher = await _publisherRepo.GetFirstAsync(predicate, trackChanges);
        //    return publisher is not null
        //    ? _mapper.Map<PublisherDto>(publisher)
        //    : null;
        //}

        //public async Task DeletePublisherAsync(Guid id)
        //{
        //    var publisher = await PublisherExistsAsync(id);
        //    _publisherRepo.Delete(publisher);
        //    await SavePublisherAsync();
        //}

        //public async Task<int> CountPublishersAsync(Expression<Func<Publisher, bool>> predicate)
        //{
        //    return await _publisherRepo.CountAsync(predicate);
        //}
        #endregion
    }
}
