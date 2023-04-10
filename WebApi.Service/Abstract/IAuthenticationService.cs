using WebApi.Core.Models;
using WebApi.Core.Models.Authentication;

namespace WebApi.Service.Abstract
{
    public interface IAuthenticationService
    {
        Task<JwtResponse> LoginAsync(LoginDto entity);
        Task RegisterAsync(RegisterDto entity);

    }
}
