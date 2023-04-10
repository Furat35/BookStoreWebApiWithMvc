using System.Linq.Expressions;
using WebApi.Core.Models.AppUser;
using WebApi.Entity.Entities;

namespace WebApi.Service.Abstract
{
    public interface IAppUserService
    {
        Task<AppUserDto> AddUserAsync(AppUserAddDto entity);
        Task UpdateUserAsync(AppUserUpdateDto entity);
        Task DeleteUserAsync(Guid id);
        Task<AppUserDto> GetUserByGuidAsync(Guid id);
        Task<IList<AppUserDto>> GetUsersAsync(Expression<Func<AppUser, bool>> predicate = null);
        Task<AppUserDto> GetFirstUserAsync(Expression<Func<AppUser, bool>> predicate);
        Task<int> CountUsersAsync(Expression<Func<AppUser, bool>> predicate);
    }
}
