using BookStore.WebMVC.Models;
using WebApi.Core.Models;
using WebApi.Core.Models.Authentication;

namespace BookStore.WebMVC.ApiServices.Abstract
{
    public interface IAuthService
    {
        Task<ResponseMessage<LoginResponse>> LoginAsync(LoginDto loginDto, HttpContext context);
        Task<ResponseMessage<RegisterDto>> RegisterAsync(RegisterDto registerDto);
        Task LogoutAsync();
    }
}
