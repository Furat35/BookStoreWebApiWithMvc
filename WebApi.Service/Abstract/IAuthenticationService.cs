using WebApi.Core.Models;
using WebApi.Core.Models.Authentication;

namespace WebApi.Service.Abstract
{
    public interface IAuthenticationService
    {
        Task<LoginResponse> LoginAsync(LoginDto entity);
        Task RegisterAsync(RegisterDto entity);

    }
}
