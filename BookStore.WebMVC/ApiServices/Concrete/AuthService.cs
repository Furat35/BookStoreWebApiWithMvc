using BookStore.WebMVC.ApiServices.Abstract;
using BookStore.WebMVC.Consts;
using BookStore.WebMVC.Extensions;
using BookStore.WebMVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Newtonsoft.Json;
using System.Security.Claims;
using WebApi.Core.Consts;
using WebApi.Core.Models;
using WebApi.Core.Models.Authentication;

namespace BookStore.ApiServices.Concrete
{
    public class AuthService : IAuthService
    {
        #region Fields
        private readonly HttpClient _httpClient;
        private readonly HttpContext _httpContext;
        #endregion

        #region Ctor
        public AuthService(IHttpClientFactory httpClient, IHttpContextAccessor httpContext)
        {
            _httpClient = httpClient.CreateClient(HttpClientConsts.DefaultName);
            _httpContext = httpContext.HttpContext;
        }
        #endregion

        public async Task<ResponseMessage<LoginResponse>> LoginAsync(LoginDto loginDto, HttpContext context)
        {
            var response = await _httpClient.PostAsJsonAsync($"{WebApiConsts.Api}/{WebApiConsts.Authentication}/{WebApiConsts.Login}", loginDto);
            string readContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(readContent);
                await AuthCookieConfigs(loginResponse, loginDto.UserName, context);
                return new ResponseMessage<LoginResponse>(true, loginResponse, null);
            }

            return ResponseMessages.ErrorResponse<LoginResponse>(readContent);

        }

        public async Task<ResponseMessage<RegisterDto>> RegisterAsync(RegisterDto registerDto)
        {
            var response = await _httpClient.PostAsJsonAsync($"{WebApiConsts.Api}/{WebApiConsts.Authentication}/{WebApiConsts.Register}", registerDto);
            string readContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var userRegister = JsonConvert.DeserializeObject<RegisterDto>(readContent);
                return new ResponseMessage<RegisterDto>(true, null, null);
            }
            return ResponseMessages.ErrorResponse<RegisterDto>(readContent);
        }

        public async Task LogoutAsync() =>
            await _httpContext.SignOutAsync();

        private async Task AuthCookieConfigs(LoginResponse response, string username, HttpContext context)
        {
            var claims = new List<Claim>()
                {
                    new Claim("Token", response.JwtResponse.Token),
                    new Claim(ClaimTypes.Name, username)
                };

            foreach (var role in response.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var claimsIdentity = new ClaimsIdentity(claims, "Login");
            await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties()
                {
                    IsPersistent = true,
                });
        }
    }
}
