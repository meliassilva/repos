using Microsoft.AspNetCore.Builder;

namespace Byui.testapi.Api.Helpers
{
    public static class StoreAuthTokenMiddlewareExtentions
    {
        public static IApplicationBuilder UseStoreAuthToken(this IApplicationBuilder app)
        {
            return app.UseMiddleware<StoreAuthTokenMiddleware>();
        }
    }
}
