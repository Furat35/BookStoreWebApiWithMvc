using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Core.Exceptions.AppUser;
using WebApi.Core.Exceptions.LoginDto;
using WebApi.Core.Exceptions.RegisterDto;
using WebApi.Core.Models;
using WebApi.Core.Models.Authentication;
using WebApi.Entity.Entities;
using WebApi.Service.Abstract;

namespace WebApi.Service.Concrete
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IValidator<LoginDto> _loginValidator;
        private readonly IValidator<RegisterDto> _registerValidator;
        private readonly IMapper _mapper;

        public AuthenticationService(
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            SignInManager<AppUser> signInManager,
            IConfiguration configuration,
            IValidator<LoginDto> _loginValidator,
            IValidator<RegisterDto> _registerValidator,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;
            this._loginValidator = _loginValidator;
            this._registerValidator = _registerValidator;
            _mapper = mapper;
        }

        public async Task<JwtResponse> LoginAsync(LoginDto entity)
        {
            var result = await _loginValidator.ValidateAsync(entity);
            if (!result.IsValid)
                throw new UnprocessableLoginDtoException("");

            var user = await _userManager.FindByNameAsync(entity.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, entity.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return (new JwtResponse()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo
                });
            }

            throw new UnauthorizedAppUserException();
        }

        public async Task RegisterAsync(RegisterDto entity)
        {
            var validationResult = await _registerValidator.ValidateAsync(entity);
            if (validationResult.IsValid)
                throw new UnprocessableRegisterDtoException(string.Join(", ", validationResult.Errors.ConvertAll(_ => _.ToString())));


            var userExists = await _userManager.FindByNameAsync(entity.UserName);
            if (userExists != null)
                throw new AppUserInternalServerError500Exception(
                    "User exists!");

            AppUser user = new()
            {
                Email = entity.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = entity.UserName
            };

            var result = await _userManager.CreateAsync(user, entity.Password);
            if (!result.Succeeded)
                throw new AppUserInternalServerError500Exception(
                    "User creation failed! Please check user details and try again.");
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
