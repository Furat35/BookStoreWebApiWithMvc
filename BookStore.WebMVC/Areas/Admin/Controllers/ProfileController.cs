using AutoMapper;
using BookStore.WebMVC.ApiServices.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Core.Models.AppUser;

namespace BookStore.WebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProfileController : Controller
    {
        #region Fields
        private readonly IProfileService _profileService;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public ProfileController(IProfileService profileService, IMapper mapper)
        {
            _profileService = profileService;
            _mapper = mapper;
        }
        #endregion

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var result = await _profileService
                .GetProfileAsync();
            return View(result.Content);
        }

        [Authorize]
        public async Task<IActionResult> Delete()
        {
            await _profileService
                .SafeDeleteProfileAsync();
            return View();
        }

        [Authorize]
        public IActionResult Account()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(AppUserUpdateDto userDto)
        {
            var result = await _profileService
                .UpdateProfileAsync(userDto);
            if (result.IsSuccess)
                return View(result.Content);

            return RedirectToAction(nameof(Account));
        }
    }
}
