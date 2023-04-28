using BookStore.WebMVC.Models;
using WebApi.Core.Models.AppUser;

namespace BookStore.WebMVC.ApiServices.Abstract
{
    public interface IProfileService
    {
        Task<ResponseMessage<AppUserDto>> GetProfileAsync();
        Task<ResponseMessage<AppUserUpdateDto>> UpdateProfileAsync(AppUserUpdateDto UserUpdateDto);
        Task<ResponseMessage<string>> SafeDeleteProfileAsync();
    }
}
