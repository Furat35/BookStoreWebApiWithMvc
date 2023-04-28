using BookStore.WebMVC.Models;
using WebApi.Core.Models.AppRole;
using WebApi.Core.RequestFilters;
using WebApi.Core.RequestFilters.Role;

namespace BookStore.WebMVC.ApiServices.Abstract
{
    public interface IRoleService
    {
        Task<(ResponseMessage<List<AppRoleDto>> response, Metadata metadata)> GetAppRolesAsync(RoleRequestFilter filter = null);
        Task<ResponseMessage<AppRoleDto>> GetAppRoleAsync(Guid id);
        Task<ResponseMessage<AppRoleAddDto>> AddAppRoleAsync(AppRoleAddDto RoleAddDto);
        Task<ResponseMessage<AppRoleUpdateDto>> UpdateAppRoleAsync(AppRoleUpdateDto RoleUpdateDto);
        Task<ResponseMessage<string>> DeleteAppRoleAsync(Guid id);
        Task<ResponseMessage<string>> SafeDeleteAppRoleAsync(Guid id);
    }
}
