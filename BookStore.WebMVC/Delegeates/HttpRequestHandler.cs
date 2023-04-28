using Newtonsoft.Json;
using WebApi.Core.Models;

namespace BookStore.WebMVC.Delegeates
{
    public class HttpRequestHandler : DelegatingHandler
    {
        #region Fields
        private readonly IHttpContextAccessor _httpContext;
        private const string token = "Authorization";
        #endregion

        #region Ctor
        public HttpRequestHandler(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        #endregion

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var isAuthenticated = _httpContext.HttpContext.User.Identity.IsAuthenticated;
            if (isAuthenticated)
            {
                var jwtToken = _httpContext.HttpContext.User.FindFirst("Token").Value;
                request.Headers.Add(token, "Bearer " + jwtToken);
            }

            var response = await base.SendAsync(request, cancellationToken);
            if ((int)response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                response.Content = new StringContent(JsonConvert.SerializeObject(new ErrorDetails
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Message = "Unauthorized user!"
                }));
            }
            return response;
        }
    }
}
