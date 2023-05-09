using BookStore.WebMVC.ApiServices.Abstract;
using BookStore.WebMVC.Models;
using UserStore.WebMVC.ApiServices.Abstract;
using WebApi.Core.Consts;
using WebApi.Core.Models.AppUser;
using WebApi.Core.RequestFilters;
using WebApi.Core.RequestFilters.User;

namespace BookStore.ApiServices.Concrete
{
    public class UserService : IUserService
    {
        #region Fields
        private readonly IHttpClientService _httpClient;
        #endregion

        #region Ctor
        public UserService(IHttpClientService httpClient)
        {
            _httpClient = httpClient;
        }
        #endregion

        public async Task<(ResponseMessage<List<AppUserDto>> response, Metadata metadata)> GetAppUsersAsync(UserRequestFilter filters = null) =>
            await _httpClient.GetAllAsync<List<AppUserDto>>($"{WebApiConsts.Api}/{WebApiConsts.AppUsers}", filters);

        public async Task<ResponseMessage<AppUserDto>> GetAppUserAsync(Guid id) =>
            await _httpClient.GetAsync<AppUserDto>($"{WebApiConsts.Api}/{WebApiConsts.AppUsers}", id.ToString());

        public async Task<ResponseMessage<AppUserAddDto>> AddAppUserAsync(AppUserAddDto AppUserAddDto) =>
            await _httpClient.PostInBodyAsync<AppUserAddDto>($"{WebApiConsts.Api}/{WebApiConsts.AppUsers}/{WebApiConsts.Add}", AppUserAddDto);

        public async Task<ResponseMessage<AppUserUpdateDto>> UpdateAppUserAsync(AppUserUpdateDto AppUserUpdateDto) =>
            await _httpClient.PostInBodyAsync<AppUserUpdateDto>($"{WebApiConsts.Api}/{WebApiConsts.AppUsers}/{WebApiConsts.Update}", AppUserUpdateDto);

        public async Task<ResponseMessage<string>> DeleteAppUserAsync(Guid id) =>
            await _httpClient.PostInRoute<string>($"{WebApiConsts.Api}/{WebApiConsts.AppUsers}/{WebApiConsts.Delete}", id.ToString());

        public async Task<ResponseMessage<string>> SafeDeleteAppUserAsync(Guid id) =>
            await _httpClient.PostInRoute<string>($"{WebApiConsts.Api}/{WebApiConsts.AppUsers}/{WebApiConsts.SafeDelete}", id.ToString());
    }
}
