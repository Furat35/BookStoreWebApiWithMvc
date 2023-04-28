using BookStore.WebMVC.ApiServices.Abstract;
using BookStore.WebMVC.Models;
using WebApi.Core.Consts;
using WebApi.Core.Models.AppUser;

namespace BookStore.WebMVC.ApiServices.Concrete
{
    public class ProfileService : IProfileService
    {
        #region Fields
        private readonly IHttpClientService _httpClient;
        private readonly HttpContext _httpContext;
        #endregion

        #region Ctor
        public ProfileService(IHttpClientService httpClient, IHttpContextAccessor httpContext)
        {
            _httpClient = httpClient;
            _httpContext = httpContext.HttpContext;
        }
        #endregion

        public async Task<ResponseMessage<AppUserDto>> GetProfileAsync() =>
            await _httpClient.GetAsync<AppUserDto>($"{WebApiConsts.Api}/{WebApiConsts.Profiles}");

        public async Task<ResponseMessage<string>> SafeDeleteProfileAsync() =>
            await _httpClient.PostInRoute<string>($"{WebApiConsts.Api}/{WebApiConsts.AppRoles}/{WebApiConsts.SafeDelete}", null);

        public async Task<ResponseMessage<AppUserUpdateDto>> UpdateProfileAsync(AppUserUpdateDto UserUpdateDto) =>
            await _httpClient.PostInBodyAsync($"{WebApiConsts.Api}/{WebApiConsts.Profiles}/{WebApiConsts.Update}", UserUpdateDto);
    }
}
