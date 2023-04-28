using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Core.ActionAttributes;
using WebApi.Core.Models.AppUser;
using WebApi.Service.Abstract;

namespace BookStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        #region Fields
        private readonly IProfileService _profileService;
        #endregion

        #region Ctor
        public ProfilesController(IProfileService profileService)
        {
            _profileService = profileService;
        }
        #endregion

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            var user = await _profileService
                .GetProfileByGuidAsync();
            return Ok(user);
        }

        [HttpPost("[action]")]
        [Authorize]
        [ValidateModelContent]
        public async Task<IActionResult> Update(AppUserUpdateDto userDto)
        {
            await _profileService
                .UpdateProfileAsync(userDto);
            return NoContent();
        }

        [HttpPost("Delete")]
        [Authorize]
        public async Task<IActionResult> SafeDelete()
        {
            await _profileService
                .SafeDeleteProfileAsync();
            return NoContent();
        }
    }
}
