using System.Security.Claims;

namespace BookStore.WebMVC.Extensions.User
{
    public static class Users
    {
        public static string GetUserId(this ClaimsPrincipal claims)
        {
            return claims.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        public static string GetUsername(this ClaimsPrincipal claims)
        {
            return claims.FindFirstValue(ClaimTypes.Name);
        }
    }
}
