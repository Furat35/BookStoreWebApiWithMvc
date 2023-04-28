using BookStore.WebMVC.ApiServices.Abstract;
using BookStore.WebMVC.Models;
using WebApi.Core.Consts;
using WebApi.Core.Models.AppRole;
using WebApi.Core.RequestFilters;
using WebApi.Core.RequestFilters.Role;

namespace BookStore.ApiServices.Concrete
{
    public class RoleService : IRoleService
    {
        #region Fields
        private readonly IHttpClientService _httpClient;
        #endregion

        #region Ctor
        public RoleService(IHttpClientService httpClient)
        {
            _httpClient = httpClient;
        }
        #endregion

        public async Task<(ResponseMessage<List<AppRoleDto>> response, Metadata metadata)> GetAppRolesAsync(RoleRequestFilter filter = null) =>
            await _httpClient.GetAllAsync<List<AppRoleDto>>($"{WebApiConsts.Api}/{WebApiConsts.AppRoles}", filter);

        public async Task<ResponseMessage<AppRoleDto>> GetAppRoleAsync(Guid id) =>
            await _httpClient.GetAsync<AppRoleDto>($"{WebApiConsts.Api}/{WebApiConsts.AppRoles}", id.ToString());

        public async Task<ResponseMessage<AppRoleAddDto>> AddAppRoleAsync(AppRoleAddDto AppRoleAddDto) =>
            await _httpClient.PostInBodyAsync<AppRoleAddDto>($"{WebApiConsts.Api}/{WebApiConsts.AppRoles}/{WebApiConsts.Add}", AppRoleAddDto);

        public async Task<ResponseMessage<AppRoleUpdateDto>> UpdateAppRoleAsync(AppRoleUpdateDto AppRoleUpdateDto) =>
            await _httpClient.PostInBodyAsync<AppRoleUpdateDto>($"{WebApiConsts.Api}/{WebApiConsts.AppRoles}/{WebApiConsts.Update}", AppRoleUpdateDto);

        public async Task<ResponseMessage<string>> DeleteAppRoleAsync(Guid id) =>
            await _httpClient.PostInRoute<string>($"{WebApiConsts.Api}/{WebApiConsts.AppRoles}/{WebApiConsts.Delete}", id.ToString());

        public async Task<ResponseMessage<string>> SafeDeleteAppRoleAsync(Guid id) =>
            await _httpClient.PostInRoute<string>($"{WebApiConsts.Api}/{WebApiConsts.AppRoles}/{WebApiConsts.SafeDelete}", id.ToString());
    }
}
