using Microsoft.AspNetCore.Mvc.Filters;
using WebApi.Service.Abstract;

namespace WebApi.Service.ActionAttributes
{
    public class RemoveCache : ActionFilterAttribute
    {
        private ICacheService _cacheService;
        private string _cacheName;
        private readonly string extension = "GetFilteredResults";
        public RemoveCache()
        {

        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            _cacheService = (ICacheService)context.HttpContext.RequestServices.GetService(typeof(ICacheService));
            _cacheName = $"{context.Controller}.{extension}";
            if (_cacheService.Exists(_cacheName))
                _cacheService.Remove(_cacheName);
        }
    }
}
