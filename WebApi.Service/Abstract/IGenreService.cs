﻿using System.Linq.Expressions;
using WebApi.Core.Models.Genres;
using WebApi.Core.RequestFilters;
using WebApi.Core.RequestFilters.Genre;
using WebApi.Entity.Entities;

namespace WebApi.Service.Abstract
{
    public interface IGenreService
    {
        Task<(List<GenreDto> genres, Metadata metadata)> GetGenresAsync(
            Expression<Func<Genre, bool>> predicate = null, GenreRequestFilter filters = null, bool trackChanges = false);
        Task<GenreDto> GetGenreByGuidAsync(Guid id);
        Task<GenreDto> GetFirstGenreAsync(Expression<Func<Genre, bool>> predicate, bool trackChanges = false);
        Task<GenreDto> AddGenreAsync(GenreAddDto entity);
        Task DeleteGenreAsync(Guid id);
        Task SafeDeleteGenreAsync(Guid id);
        Task UpdateGenreAsync(GenreUpdateDto entity);
        Task<int> CountGenresAsync(Expression<Func<Genre, bool>> predicate);
    }
}
