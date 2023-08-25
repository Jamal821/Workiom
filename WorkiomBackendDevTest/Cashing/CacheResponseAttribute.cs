using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace WorkiomBackendDevTest.Cashing
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class CacheAttribute : Attribute, IActionFilter
    {
        private readonly IMemoryCache _cache;
        private readonly int _cacheDurationMinutes;

        public CacheAttribute(int cacheDurationMinutes)
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
            _cacheDurationMinutes = cacheDurationMinutes;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var cacheKey = GenerateCacheKey(context);
            var cachedData = _cache.Get(cacheKey);
            if (cachedData != null)
            {
                context.Result = new OkObjectResult(cachedData);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is OkObjectResult okResult)
            {
                var cacheKey = GenerateCacheKey(context);
                _cache.Set(cacheKey, okResult.Value, TimeSpan.FromMinutes(_cacheDurationMinutes));
            }
        }

        private string GenerateCacheKey(FilterContext context)
        {
            var request = context.HttpContext.Request;
            return $"{request.Path}{request.QueryString}";
        }
    }
}
