using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApi.Core.Exceptions;
using WebApi.Core.Exceptions.AppUser;
using WebApi.Core.Models.AppUser;
using WebApi.Core.RequestFilters;
using WebApi.Core.RequestFilters.User;
using WebApi.Entity.Entities;
using WebApi.Service.Abstract;
using static WebApi.Core.Consts.RoleConsts;

namespace WebApi.Service.Concrete
{
    public sealed class AppUserService : IAppUserService
    {
        #region Fields
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppRoleService _roleService;
        private readonly IValidator<AppUser> _validator;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public AppUserService(UserManager<AppUser> userManager, IMapper mapper, IAppRoleService roleService
            , IValidator<AppUser> validator)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleService = roleService;
            _validator = validator;
        }
        #endregion

        public async Task<AppUserDto> AddUserAsync(AppUserAddDto entity)
        {
            var map = _mapper.Map<AppUser>(entity);
            await AppUserValidatorAsync(map);
            var userExists = await _userManager
                .FindByNameAsync(entity.UserName);
            if (userExists is not null)
                throw new AppUserAlreadyExistsException(entity.UserName);

            var user = _mapper.Map<AppUser>(entity);
            await CreateUserAsync(user, entity.Password);
            var roleResult = await _userManager
                .AddToRoleAsync(user, UserRole);
            if (!roleResult.Succeeded)
                throw new AppUserInternalServerError500Exception();

            return _mapper.Map<AppUserDto>(user);
        }

        public async Task UpdateUserAsync(AppUserUpdateDto entity)
        {
            var map = _mapper.Map<AppUser>(entity);
            await AppUserValidatorAsync(map);
            var user = await UserExists(id: entity.Id);
            _mapper.Map(entity, user);
            await UpdateUserAsync(user);
        }

        public async Task SafeDeleteUserAsync(Guid id)
        {
            var user = await UserExists(id: id);
            if (!user.IsDeleted)
            {
                user.IsDeleted = true;
                await UpdateUserAsync(user);
            }
        }

        public async Task<AppUserDto> GetUserByGuidAsync(Guid id)
        {
            var user = await UserExists(id: id);
            return _mapper.Map<AppUserDto>(user);
        }

        public async Task<(List<AppUserDto> users, Metadata metadata)> GetUsersAsync(
            Expression<Func<AppUser, bool>> predicate = null, UserRequestFilter filters = null)
        {
            var users = _userManager
                .Users
                .AsNoTracking();
            if (predicate != null)
                users = users.Where(predicate);

            var userSkip = (filters.Page - 1) * filters.PageSize;
            bool isValidPage = users.Count() > userSkip
                ? true
                : false;

            if (!isValidPage)
                throw new InvalidPageException();

            Metadata metadata = new Metadata()
            {
                CurrentPage = filters.Page,
                PageSize = filters.PageSize,
                //TotalPages = publishers.Count() / filters.PageSize,
                TotalEntities = users.Count()
            };

            return users is not null
            ? (_mapper.Map<List<AppUserDto>>(await users.ToListAsync()), metadata)
            : (null, metadata);
        }

        public async Task<AppUserDto> GetFirstUserAsync(Expression<Func<AppUser, bool>> predicate)
        {
            var user = await _userManager.Users
                .Where(predicate)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return user is not null
                ? _mapper.Map<AppUserDto>(user)
                : null;
        }

        #region Private Methods
        private async Task AddToRoleAsync(Guid id, string roleName)
        {
            var role = await _roleService
                .GetRoleByNameAsync(roleName);
            var user = await UserExists(id: id);
            var result = await _userManager
                .AddToRoleAsync(user, role.Name);

            if (!result.Succeeded)
                throw new AppUserInternalServerError500Exception($"Error occured while trying to add role to user : {id}");
        }

        private async Task UpdateUserAsync(AppUser user)
        {
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                throw new AppUserInternalServerError500Exception($"Error occured while trying to delete user : {user.UserName}");
        }

        private async Task CreateUserAsync(AppUser user, string password)
        {
            var result = await _userManager
                .CreateAsync(user, password);
            if (!result.Succeeded)
                throw new AppUserInternalServerError500Exception($"Error occured while trying to create user : {user.UserName}");
        }

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
            var result = await _validator.ValidateAsync(appUser);
            if (!result.IsValid)
                throw new UnprocessableAppUserException();
        }
        #endregion

        #region Methods that can be needed later
        //public async Task<int> CountUsersAsync(Expression<Func<AppUser, bool>> predicate)
        //{
        //    return await _userManager.Users
        //        .Where(predicate)
        //        .AsNoTracking()
        //        .CountAsync();
        //}
        #endregion
    }
}
