using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApi.Core.Exceptions.AppRole;
using WebApi.Core.Models.AppRole;
using WebApi.Entity.Entities;
using WebApi.Service.Abstract;

namespace WebApi.Service.Concrete
{
    public class AppRoleService : IAppRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IValidator<AppRole> _validator;

        public AppRoleService(RoleManager<AppRole> roleManager, IMapper mapper, IValidator<AppRole> validator)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<IList<AppRoleDto>> GetRolesAsync(Expression<Func<AppRole, bool>> predicate = null)
        {
            var roles = await _roleManager.Roles
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync();
            return roles is not null
            ? _mapper.Map<List<AppRoleDto>>(roles)
            : null;
        }

        public async Task<AppRoleDto> GetRoleByNameAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role is null)
                throw new AppRoleNotFoundException(roleName);

            return _mapper.Map<AppRoleDto>(role);
        }

        public async Task<AppRoleDto> GetRoleByGuid(Guid id)
        {
            var role = await RoleExistsAsync(id);
            return _mapper.Map<AppRoleDto>(role);
        }

        public async Task<AppRoleDto> AddRoleAsync(AppRoleAddDto entity)
        {
            var roleExists = await _roleManager.FindByNameAsync(entity.Name);
            if (roleExists is not null)
                throw new AppRoleAlreadyExistsException(entity.Name);

            var role = _mapper.Map<AppRole>(entity);
            await AppRoleValidatorAsync(role);
            var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded)
                throw new AppRoleInternalServerError500Exception($"Error occured while trying to create role : {role.Name}");

            return _mapper.Map<AppRoleDto>(role);
        }

        public async Task DeleteRoleAsync(Guid id)
        {
            var role = await RoleExistsAsync(id: id);
            role.IsDeleted = true;
            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded)
                throw new AppRoleInternalServerError500Exception($"Error occured while trying to delete role : {role.Name}");
        }

        public async Task UpdateRoleAsync(AppRoleUpdateDto entity)
        {
            var map = _mapper.Map<AppRole>(entity);
            await AppRoleValidatorAsync(map);
            var role = await RoleExistsAsync(id: entity.Id);
            _mapper.Map(entity, role);
            var result = await _roleManager.UpdateAsync(role);

            if (!result.Succeeded)
                throw new AppRoleInternalServerError500Exception($"Error occured while trying to update role : {role.Name}");
        }

        private async Task<AppRole> RoleExistsAsync(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role is null)
                throw new AppRoleNotFoundException(id.ToString());

            return role;
        }

        private async Task AppRoleValidatorAsync(AppRole appRole)
        {
            var result = await _validator.ValidateAsync(appRole);
            if (!result.IsValid)
                throw new UnprocessableAppRoleException();
        }
    }
}
