using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApi.Service.Abstract;

namespace WebApi.Service.ActionAttributes
{
    public class CacheDataAttribute : ActionFilterAttribute
    {
        public int Duration { get; set; }
        public Type returnValue { get; set; }
        private ICacheService _cacheService;
        private string _cacheName;
        private readonly string extension = "GetFilteredResults";

        public CacheDataAttribute()
        {
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            _cacheService = (ICacheService)context.HttpContext.RequestServices.GetService(typeof(ICacheService));
            var a = _cacheService.Get("QueryParams");
            var b = context.HttpContext.Request.QueryString.Value;
            _cacheName = $"{context.Controller}.{extension}";
            if (_cacheService.Exists(_cacheName) && _cacheService.Get("QueryParams").ToString() == context.HttpContext.Request.QueryString.Value)
            {
                context.HttpContext.Response.Headers.Add("X-Pagination", _cacheService.Get("X-Pagination").ToString());
                var result = _cacheService.Get(_cacheName);
                result = result.GetType().GetProperty("Value").GetValue(result);
                context.Result = new OkObjectResult(result);
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is null)
            {
                base.OnActionExecuted(context);
                _cacheService = (ICacheService)context.HttpContext.RequestServices.GetService(typeof(ICacheService));
                _cacheName = $"{context.Controller}.{extension}";
                _cacheService.Add(_cacheName, context.Result);
                _cacheService.Add("X-Pagination", context.HttpContext.Response.Headers["X-Pagination"]);
                _cacheService.Add("QueryParams", context.HttpContext.Request.QueryString.Value);
            }


        }
    }
}
