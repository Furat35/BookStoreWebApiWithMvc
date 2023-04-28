using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WebApi.Core.Exceptions.AppUser;
using WebApi.Core.Models.AppUser;
using WebApi.Entity.Entities;
using WebApi.Service.Abstract;

namespace WebApi.Service.Concrete
{
    public sealed class ProfileService : IProfileService
    {
        #region Fields
        private readonly HttpContext _httpContext;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IValidator<AppUser> _validator;
        private readonly Guid id;
        #endregion

        #region Ctor
        public ProfileService(IHttpContextAccessor httpContext, IMapper mapper, UserManager<AppUser> userManager, IValidator<AppUser> validator)
        {
            _httpContext = httpContext.HttpContext;
            _mapper = mapper;
            _userManager = userManager;
            _validator = validator;
            id = Guid.Parse(_httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
        #endregion

        public async Task<AppUserDto> GetProfileByGuidAsync()
        {
            var user = await _userManager
                .FindByIdAsync(id.ToString());
            return _mapper.Map<AppUserDto>(user);
        }

        public async Task SafeDeleteProfileAsync()
        {
            var user = await UserExists(id);
            user.IsDeleted = true;
            await UpdateUser(user);
        }

        public async Task UpdateProfileAsync(AppUserUpdateDto entity)
        {
            var map = _mapper.Map<AppUser>(entity);
            await AppUserValidatorAsync(map);
            var user = await _userManager.FindByIdAsync(id.ToString());
            _mapper.Map(entity, user);
            user.Id = id;
            await UpdateUser(user);
        }

        #region Private Methods
        private async Task<AppUser> UserExists(Guid id)
        {
            var user = await _userManager
                .FindByIdAsync(id.ToString());
            if (user is null)
                throw new AppUserNotFoundException(id.ToString());

            return user;
        }

        private async Task AppUserValidatorAsync(AppUser appUser)
        {
            var result = await _validator
                .ValidateAsync(appUser);
            if (!result.IsValid)
                throw new UnprocessableAppUserException();
        }

        private async Task UpdateUser(AppUser user)
        {
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                throw new AppUserInternalServerError500Exception($"Error occured while trying to update user : {user.UserName}");
        }
        #endregion
    }
}
