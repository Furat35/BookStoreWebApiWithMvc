using System.Linq.Expressions;
using WebApi.Core.Models.AppUser;
using WebApi.Core.RequestFilters;
using WebApi.Core.RequestFilters.User;
using WebApi.Entity.Entities;

namespace WebApi.Service.Abstract
{
    public interface IAppUserService
    {
        Task<AppUserDto> AddUserAsync(AppUserAddDto entity);
        Task UpdateUserAsync(AppUserUpdateDto entity);
        Task SafeDeleteUserAsync(Guid id);
        Task<AppUserDto> GetUserByGuidAsync(Guid id);
        Task<(List<AppUserDto> users, Metadata metadata)> GetUsersAsync(Expression<Func<AppUser, bool>> predicate = null, UserRequestFilter filters = null);
        Task<AppUserDto> GetFirstUserAsync(Expression<Func<AppUser, bool>> predicate);
        //Task<int> CountUsersAsync(Expression<Func<AppUser, bool>> predicate);
    }
}
