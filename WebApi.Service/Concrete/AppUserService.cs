using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApi.Core.Exceptions.AppUser;
using WebApi.Core.Models.AppUser;
using WebApi.Entity.Entities;
using WebApi.Service.Abstract;
using static WebApi.Core.Consts.RoleConsts;

namespace WebApi.Service.Concrete
{
    public class AppUserService : IAppUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppRoleService _roleService;
        private readonly IValidator<AppUser> _validator;
        private readonly IMapper _mapper;

        public AppUserService(UserManager<AppUser> userManager, IMapper mapper, IAppRoleService roleService
            , IValidator<AppUser> validator)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleService = roleService;
            _validator = validator;
        }
        public async Task<AppUserDto> AddUserAsync(AppUserAddDto entity)
        {
            var map = _mapper.Map<AppUser>(entity);
            await AppUserValidatorAsync(map);
            var userExists = await _userManager.FindByNameAsync(entity.UserName);
            if (userExists is not null)
                throw new AppUserAlreadyExistsException(entity.UserName);

            var user = _mapper.Map<AppUser>(entity);
            var result = await _userManager.CreateAsync(user, entity.Password);
            if (!result.Succeeded)
                throw new AppUserInternalServerError500Exception($"Error occured while trying to create user : {entity.UserName}");

            var roleResult = await _userManager.AddToRoleAsync(user, UserRole);
            if (!roleResult.Succeeded)
                throw new AppUserInternalServerError500Exception();

            return _mapper.Map<AppUserDto>(user);
        }

        public async Task AddToRoleAsync(Guid id, string roleName)
        {
            var role = await _roleService.GetRoleByNameAsync(roleName);
            var user = await UserExists(id: id);
            var result = await _userManager.AddToRoleAsync(user, role.Name);

            if (!result.Succeeded)
                throw new AppUserInternalServerError500Exception($"Error occured while trying to add role to user : {id}");
        }

        public async Task UpdateUserAsync(AppUserUpdateDto entity)
        {
            var map = _mapper.Map<AppUser>(entity);
            await AppUserValidatorAsync(map);
            var user = await UserExists(id: entity.Id);
            _mapper.Map(entity, user);
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                throw new AppUserInternalServerError500Exception($"Error occured while trying to update user : {entity.UserName}");
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await UserExists(id: id);
            if (!user.IsDeleted)
            {
                user.IsDeleted = true;
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                    throw new AppUserInternalServerError500Exception($"Error occured while trying to delete user : {user.UserName}");
            }
        }

        public async Task<AppUserDto> GetUserByGuidAsync(Guid id)
        {
            var user = await UserExists(id: id);
            return _mapper.Map<AppUserDto>(user);
        }

        public async Task<IList<AppUserDto>> GetUsersAsync(Expression<Func<AppUser, bool>> predicate = null)
        {
            var users = _userManager.Users.AsNoTracking();
            if (predicate != null)
                users = users.Where(predicate);

            return users is not null
            ? _mapper.Map<List<AppUserDto>>(await users.ToListAsync())
            : null;
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

        public async Task<int> CountUsersAsync(Expression<Func<AppUser, bool>> predicate)
        {
            return await _userManager.Users
                .Where(predicate)
                .AsNoTracking()
                .CountAsync();
        }

        private async Task<AppUser> UserExists(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
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
    }
}
