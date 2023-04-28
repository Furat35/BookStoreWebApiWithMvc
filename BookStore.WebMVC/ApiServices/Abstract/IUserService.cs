using BookStore.WebMVC.Models;
using WebApi.Core.Models.AppUser;
using WebApi.Core.RequestFilters;
using WebApi.Core.RequestFilters.User;

namespace UserStore.WebMVC.ApiServices.Abstract
{
    public interface IUserService
    {
        Task<(ResponseMessage<List<AppUserDto>> response, Metadata metadata)> GetAppUsersAsync(UserRequestFilter filters = null);
        Task<ResponseMessage<AppUserDto>> GetAppUserAsync(Guid id);
        Task<ResponseMessage<AppUserAddDto>> AddAppUserAsync(AppUserAddDto UserAddDto);
        Task<ResponseMessage<AppUserUpdateDto>> UpdateAppUserAsync(AppUserUpdateDto UserUpdateDto);
        Task<ResponseMessage<string>> DeleteAppUserAsync(Guid id);
        Task<ResponseMessage<string>> SafeDeleteAppUserAsync(Guid id);
    }
}
