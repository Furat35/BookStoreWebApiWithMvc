using System.Linq.Expressions;
using WebApi.Core.Models.AppRole;
using WebApi.Entity.Entities;

namespace WebApi.Service.Abstract
{
    public interface IAppRoleService
    {
        Task<IList<AppRoleDto>> GetRolesAsync(Expression<Func<AppRole, bool>> predicate = null);
        Task<AppRoleDto> GetRoleByNameAsync(string roleName);
        Task<AppRoleDto> GetRoleByGuid(Guid id);
        Task<AppRoleDto> AddRoleAsync(AppRoleAddDto entity);
        Task DeleteRoleAsync(Guid id);
        Task UpdateRoleAsync(AppRoleUpdateDto entity);

    }
}
