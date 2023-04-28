using System.Linq.Expressions;
using WebApi.Core.Models.AppRole;
using WebApi.Core.RequestFilters;
using WebApi.Core.RequestFilters.Role;
using WebApi.Entity.Entities;

namespace WebApi.Service.Abstract
{
    public interface IAppRoleService
    {
        Task<(List<AppRoleDto> roles, Metadata metadata)> GetRolesAsync(Expression<Func<AppRole, bool>> predicate = null, RoleRequestFilter filter = null);
        Task<AppRoleDto> GetRoleByNameAsync(string roleName);
        Task<AppRoleDto> GetRoleByGuid(Guid id);
        Task<AppRoleDto> AddRoleAsync(AppRoleAddDto entity);
        Task SafeDeleteRoleAsync(Guid id);
        Task UpdateRoleAsync(AppRoleUpdateDto entity);

    }
}
