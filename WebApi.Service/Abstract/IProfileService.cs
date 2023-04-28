using WebApi.Core.Models.AppUser;

namespace WebApi.Service.Abstract
{
    public interface IProfileService
    {
        Task<AppUserDto> GetProfileByGuidAsync();
        Task SafeDeleteProfileAsync();
        Task UpdateProfileAsync(AppUserUpdateDto entity);
    }
}
