﻿using System.Linq.Expressions;
using WebApi.Core.Models.Publisher;
using WebApi.Core.RequestFilters;
using WebApi.Core.RequestFilters.Publisher;
using WebApi.Entity.Entities;

namespace WebApi.Service.Abstract
{
    public interface IPublisherService
    {
        Task<(List<PublisherDto> publishers, Metadata metadata)> GetPublishersAsync(
            Expression<Func<Publisher, bool>> predicate = null, PublisherRequestFilter filters = null);
        Task<PublisherDto> GetPublisherByGuidAsync(Guid id);

        Task<PublisherDto> AddPublisherAsync(PublisherAddDto entity);
        Task SafeDeletePublisherAsync(Guid id);
        Task UpdatePublisherAsync(PublisherUpdateDto entity);
        //Task<PublisherDto> GetFirstPublisherAsync(Expression<Func<Publisher, bool>> predicate, bool trackChanges = false);
        //Task DeletePublisherAsync(Guid id);
        //Task<int> CountPublishersAsync(Expression<Func<Publisher, bool>> predicate);
    }
}
