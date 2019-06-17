using Microsoft.AspNetCore.Builder;

namespace Byui.StudentListApi.Api.Helpers
{
    public static class StoreAuthTokenMiddlewareExtentions
    {
        public static IApplicationBuilder UseStoreAuthToken(this IApplicationBuilder app)
        {
            return app.UseMiddleware<StoreAuthTokenMiddleware>();
        }
    }
}
