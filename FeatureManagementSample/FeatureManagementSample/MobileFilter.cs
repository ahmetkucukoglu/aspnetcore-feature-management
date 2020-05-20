namespace FeatureManagementSample
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.FeatureManagement;
    using System;
    using System.Threading.Tasks;

    public class MobileFilter : IFeatureFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MobileFilter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
        {
            var isEnabled = false;

            var settings = context.Parameters.Get<MobileFilterSettings>();

            var userAgent = _httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();

            foreach (var item in settings.Allowed)
            {
                if (userAgent.Contains(item, StringComparison.OrdinalIgnoreCase))
                {
                    isEnabled = true;
                    break;
                }
            }

            return Task.FromResult(isEnabled);
        }
    }
}
